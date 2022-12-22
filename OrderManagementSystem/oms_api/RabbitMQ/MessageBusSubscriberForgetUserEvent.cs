using System.Text;
using System.Text.Json;
using OrderManagementSystem.Logic;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace OrderManagementSystem.API.RabbitMQ
{
    public class MessageBusSubscriberForgetUserEvent: BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriberForgetUserEvent(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;

            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"]),
                UserName = _configuration["RabbitMQUser"],
                Password = _configuration["RabbitMQPassword"]
            };

            Console.WriteLine(factory.HostName);
            Console.WriteLine(factory.Port);
            Console.WriteLine(factory.UserName);
            Console.WriteLine(factory.Password);

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Direct);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "ForgetUserEvent");

            Console.WriteLine("--> Listening on the Message Bus...");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("--> Event Received!");

                var body = ea.Body;
                var notificationMassage = Encoding.UTF8.GetString(body.ToArray());

                using (var scope = _scopeFactory.CreateScope())
                {
                    var orderManager = scope.ServiceProvider.GetRequiredService<IOrderManager>();

                    var userId = JsonSerializer.Deserialize<Guid>(notificationMassage);

                    orderManager.ForgetUser(userId);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Connection Shutdown");
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }
    }
}
