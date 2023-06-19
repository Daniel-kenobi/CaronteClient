using Caronte.Domain.Models.Client;
using Caronte.Domain.Responses;
using MediatR;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommand : IRequest<CommonResponse>
    {
        public ClientModel ClientInformation { get; set; }
    }
}
