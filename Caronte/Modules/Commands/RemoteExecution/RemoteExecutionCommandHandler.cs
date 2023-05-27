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
using System;
using Barsa.Models.Errors;
using System.Collections.Generic;

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
            var response = new CommonResponse();

            try
            {
                await ConfigureQueueAndExecution(request.ClientModel);
            }
            catch (Exception ex)
            {
                response.AddErrors(new Errors(ErrorType.Unspecified, ex?.InnerException?.Message ?? ex?.Message, new List<Exception>() { ex }));
            }

            return response;
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

        private async Task ConfigureExecution(ClientCommand command)
        {

        }
    }
}
