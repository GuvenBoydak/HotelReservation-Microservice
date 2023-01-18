using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class RabbitMqPersistentConnection : IDisposable
    {
        private IConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private object lock_object = new object();
        private readonly int _retryCount;
        private bool _disposed;

        public RabbitMqPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _retryCount = retryCount;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen;

        
        public IModel CreateModel()
        {
            return _connection.CreateModel();
        } 

        public void Dispose()
        {
            _disposed = true;
            _connection?.Dispose();
        }

        public bool TryConnect()
        {   
            lock (lock_object)//TryConnect çalıştıgında bu işlemin bitmesini bekliycek
            {
                //Retry mekanızması kuruyoruz.
                var policy = Policy.Handle<SocketException>()// SocketException,BrokerUnreachableException hataları alındıgında tekrar edilicek  policy.Execute çalışıcak.
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {}
                );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    _connection.CallbackException += Connection_CallbackException;//Baglantı da Bir Exeption oldugunda bu event çalışıcak.
                    _connection.ConnectionBlocked += Connection_ConnectionBlocked;//baglantıda bir blocklanma oldugunda bu event çalışıcak.
                    _connection.ConnectionShutdown += Connection_ConnectionShutdown;//Baglantı koptugunda bu event çalışıcak.
                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionBlocked(object sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }

        private void Connection_CallbackException(object sender, global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (!_disposed) return;

            TryConnect();
        }
    }
}