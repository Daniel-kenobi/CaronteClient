using Caronte.Modules.Command.ReceiveCommand;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Command
{
    public static class StartCommandServices
    {
        public static Task InitializeReceiveCommandTask(CancellationToken _cancellationToken, IMediator _mediator)
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
    }
}
