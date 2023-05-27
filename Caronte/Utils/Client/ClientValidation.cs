using Caronte.Modules.Information.GetClientInformation;
using Caronte.Modules.ValidateClient;
using MediatR;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte.Utils.Client
{
    public class ClientValidation
    {
        private readonly IMediator _mediator;
        public ClientValidation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Validate()
        {
            var clientInformation = await _mediator.Send(new GetClientInformationQuery());
            var verifyAndCreateClientResponse = await _mediator.Send(new ValidateClientCommand() { ClientInformation = clientInformation.ResponseObject });

            if (!verifyAndCreateClientResponse.IsSucessFull)
            {
                Application.Exit();
                return;
            }
        }
    }
}
