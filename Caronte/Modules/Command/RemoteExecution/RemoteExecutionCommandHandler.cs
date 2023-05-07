using Barsa.Commoms;
using Barsa.Models.User;
using Caronte.Modules.Command.RemoteExecution;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class RemoteExecutionCommandHandler : IRequestHandler<RemoteExecutionCommand, CommomResponse>
    {
        private void ConfigureQueue(UserLoginModel userModel)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(userModel.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var serializedExecutionObject = JsonSerializer.Deserialize<UserCommand>(message);
                        Execute(serializedExecutionObject);
                    };

                    var queueToConsume = userModel.ToString();
                    channel.BasicConsume(queueToConsume, autoAck: true, consumer: consumer);
                }
            }
        }

        public void Execute(UserCommand command)
        {

        }

        public Task<CommomResponse> Handle(RemoteExecutionCommand request, CancellationToken cancellationToken)
        {
            ConfigureQueue(new UserLoginModel() { });
            Execute(new UserCommand() { });

            return Task.FromResult(new CommomResponse());
        }
    }
}
