﻿using Barsa.Commons;
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
using Caronte.Modules.Commands.RemoteExecution.Factory;
using Barsa.Interfaces;

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
                response.AddErrors(new Errors(ErrorType.Unspecified, ex?.InnerException?.Message ?? ex.Message, new List<Exception>() { ex }));
            }

            return Task.FromResult(response);
        }

        private void ConfigureQueueExecution(ClientModel clientModel)
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
