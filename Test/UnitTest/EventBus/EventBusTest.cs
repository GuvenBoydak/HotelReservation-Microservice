using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.EventBus.Events.EventHandlers;
using UnitTest.EventBus.Events.Events;


namespace UnitTest.EventBus;

public class EventBusTest
{
    private ServiceCollection _services;

    public EventBusTest()
    {
        _services = new ServiceCollection();
    }

    [Fact]
    public void subscribe_event_on_rabbitmq_test()
    {
        _services.AddSingleton<IEventBus>(sp =>
        {
            EventBusConfig config = new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                EventNameSuffix = "IntegrationEvent",
                SubscriberClientAppName = "EventBus.UnitTest",
                DefaultTopicName = "Tests"
            };
            return new EventBusRabbitMq(config, sp);
        });

        var sp = _services.BuildServiceProvider();

        var eventBus = sp.GetRequiredService<IEventBus>();

        eventBus.Subscribe<TestsIntegrationEvent, TestsIntegrationEventHandler>();
    }

    [Fact]
    public void Send_message_to_rabbitMq()
    {
        _services.AddSingleton<IEventBus>(sp =>
        {
            EventBusConfig config = new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                SubscriberClientAppName = "EventBus.UnitTest",
                DefaultTopicName = "Tests",
                EventNameSuffix = "IntegrationEvent"
            };
            return new EventBusRabbitMq(config, sp);
        });

        var sp = _services.BuildServiceProvider();

        var eventBus = sp.GetRequiredService<IEventBus>();

        eventBus.Publish(new TestsIntegrationEvent(1));
    }
}