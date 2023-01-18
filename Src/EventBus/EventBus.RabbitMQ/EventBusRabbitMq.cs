using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ;

public class EventBusRabbitMq:BaseEventBus
{
    private readonly RabbitMqPersistentConnection _persistentConnection;
    private readonly IModel _consumerChannel;
    public EventBusRabbitMq(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
    {
        ConnectionFactory connectionFactory;
        if (config.Connection != null)
        {
            var connJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
        }
        else
            connectionFactory = new ConnectionFactory();

        _persistentConnection = new RabbitMqPersistentConnection(connectionFactory, config.ConnectionRetryCount);

        _consumerChannel = CreateConsumerChannel();

        SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
    }

    private void SubsManager_OnEventRemoved(object sender, string eventName)
    {
        eventName = ProcessEventName(eventName);

        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }
        
        _consumerChannel.QueueUnbind(queue: eventName, exchange: EventBusConfig.DefaultTopicName, routingKey: eventName);

        if (SubsManager.IsEmpty)
        {
            _consumerChannel.Close();
        }
    }

    public override void Publish(IntegrationEvent @event)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>() // SocketException,BrokerUnreachableException hataları alındıgında tekrar edilicek;
            .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                //logging
            });

        var eventName = @event.GetType().Name;
        eventName = ProcessEventName(eventName);

        //Excahnge olamama ihtimaline karşı create ediyoruz.
        _consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");
        
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);
        
        policy.Execute(() =>
        {
            var properties = _consumerChannel.CreateBasicProperties();
            properties.DeliveryMode = 2;
            
            _consumerChannel.BasicPublish(
                exchange: EventBusConfig.DefaultTopicName,//Exchange tipi
                routingKey: eventName,//routingKey 
                mandatory: true,
                basicProperties: properties,//veri kalıcılıgı sagladık
                body: body); //mesaj
        });
    }

    public override void Subscribe<T, TH>()
    {
        var eventName=typeof(T).Name; 
        eventName = ProcessEventName(eventName);

        if (!SubsManager.HasSubscriptionForEvent(eventName))
        {
            if (!_persistentConnection.IsConnected)
            { 
                _persistentConnection.TryConnect();
            }
            
            _consumerChannel.QueueDeclare(queue: GetSubName(eventName), durable: true, exclusive: false, autoDelete: false, arguments: null);
            //Oluşturulan kuyruga exchange ve routingkey ile bind oluyoruz.
            _consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: EventBusConfig.DefaultTopicName, routingKey: eventName);
        }

        SubsManager.AddSubscription<T, TH>();
        StartBasicConsume(eventName);
    }

    public override void UnSubscribe<T, TH>()
    {
        SubsManager.RemoveSubscription<T, TH>();
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
        { 
            _persistentConnection.TryConnect();
        }

        var channel = _persistentConnection.CreateModel();
        
        channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");
        //Direct Exchange: “routing key”belirlenir ve bu anahtar bilgisi kuyruğa yazılır. “consumer” tarafından da bu anahtarlara göre işlem yapılır.

        return channel;
    }

    private void StartBasicConsume(string eventName)
    {
        if (_consumerChannel != null)
        {
            var consumer = new EventingBasicConsumer(_consumerChannel);

            consumer.Received += Consumer_Received;//Mesaj alındıgında bu event çalışıcak

            _consumerChannel.BasicConsume(queue: GetSubName(eventName), autoAck: false, consumer: consumer);
        }
    }

    private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        eventName = ProcessEventName(eventName);

        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            await ProcessEvent(eventName, message);
        }
        catch (Exception)
        {
            ///logging
        }

        _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);//Mesaj başaroıyla işlendikten sonra silinicek.
    }
}