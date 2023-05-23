using Barsa.Commons;
using Barsa.Models.Client;
using MediatR;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommand : IRequest<CommonResponse>
    {
        public ClientModel ClientInformation { get; set; }
    }
}
