using Caronte.Modules.Logger;
using Caronte.Modules.PrintScreen;
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

            await Task.WhenAll(logTask, printScreenTask);
        }

        private static Task InitializeLogTask()
        {
            var logTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await _mediator.Send(new LogQuery());
                    await Task.Delay(TimeSpan.FromSeconds(1), _cancellationToken);
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
                    await _mediator.Send(new PrintScreenQuery() { SecondsToGetScreenshots = 5});
                    await Task.Delay(TimeSpan.FromSeconds(1), _cancellationToken);
                }
            }, _cancellationToken);

            return printScreenTask;
        }
    }
}
