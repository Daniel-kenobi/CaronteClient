using Barsa.Models.Client;
using Barsa.Commons;
using MediatR;
using Barsa.Models.Client;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommand : IRequest<CommonResponse>
    {
        public ClientModel ClientInformation { get; set; }
    }
}
