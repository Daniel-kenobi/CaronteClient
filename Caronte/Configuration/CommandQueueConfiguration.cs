using Barsa.Models.User;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Caronte.Configuration
{
    public class CommandQueueConfiguration
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly UserModel _user;
        public CommandQueueConfiguration(UserModel user)
        {
            _user = user;
            _connectionFactory = new()
            {
                HostName = "localhost"
            };
        }

        public void Configure()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _user.Username, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var commandObject = JsonSerializer.Deserialize<UserCommand>(message);

                    };

                    channel.BasicConsume(queue: _user.ToString(), autoAck: true, consumer: consumer);
                }
            }
        }
    }
}
