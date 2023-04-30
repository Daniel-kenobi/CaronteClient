using Barsa.Models.User;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace Caronte.Configuration
{
    public class CommandQueueConfiguration
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly UserModel _user;
        private readonly IMediator _mediator;

        public CommandQueueConfiguration(UserModel user, IMediator mediator)
        {
            _user = user;
            _mediator = mediator;
            _connectionFactory = new()
            {
                HostName = "localhost"
            };
        }

        public async Task Configure()
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
                        
                    };

                    channel.BasicConsume(queue: _user.ToString(), autoAck: true, consumer: consumer);
                }
            }
        }
    }
}
