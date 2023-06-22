using Caronte.Domain.Enums;
using Caronte.Domain.Models.Client;
using Caronte.Domain.Models.Errors;
using Caronte.Domain.Responses;
using Caronte.Modules.Command.RemoteExecution;
using Caronte.Modules.Commands.RemoteExecution.Factory;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class RemoteExecutionCommandHandler : IRequestHandler<RemoteExecutionCommand, CommonResponse>
    {
        public Task<CommonResponse> Handle(RemoteExecutionCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponse();

            try
            {
                ConfigureQueueExecution(request.ClientModel);
            }
            catch (Exception ex)
            {
                response.AddErrors(new Error(ErrorTypeEnum.Unspecified, ex?.InnerException?.Message ?? ex.Message, new List<Exception>() { ex }));
            }

            return Task.FromResult(response);
        }

        private void ConfigureQueueExecution(Domain.Models.Client.ClientModel clientModel)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(clientModel.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        ConfigureExecution(CreateMessageBody(ea));
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

        private void ConfigureExecution(ClientCommand command)
        {
            var executionCommandFactory = new ExecutionCommandFactory(command.Command);
            var commandToExecute = executionCommandFactory.CreateCommand();
            
            commandToExecute.Execute(command);
        }
    }
}
