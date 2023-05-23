using Barsa.Commons;
using Barsa.Models.Client;
using Caronte.Modules.Command.RemoteExecution;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class RemoteExecutionCommandHandler : IRequestHandler<RemoteExecutionCommand, CommonResponse>
    {
        private readonly IMediator _mediator;
        public RemoteExecutionCommandHandler(IMediator mediator)
        {
            _mediator = mediator;    
        }

        public async Task<CommonResponse> Handle(RemoteExecutionCommand request, CancellationToken cancellationToken)
        {
            await ConfigureQueueAndExecution(request.ClientModel);
        }

        private async Task ConfigureQueueAndExecution(ClientModel clientModel)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(clientModel.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (sender, ea) =>
                    {
                        await ConfigureExecution(CreateMessageBody(ea));
                    };

                    var queueToConsume = clientModel.ToString();
                    channel.BasicConsume(queueToConsume, autoAck: true, consumer: consumer);
                }
            }
        }

        private ClientCommand CreateMessageBody(BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            return JsonSerializer.Deserialize<ClientCommand>(message);
        }

        public async Task ConfigureExecution(ClientCommand command)
        {

        }
    }
}
