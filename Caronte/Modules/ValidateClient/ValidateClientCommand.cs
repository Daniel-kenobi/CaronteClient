using Barsa.Models.User;
using Barsa.Commoms;
using MediatR;
using Barsa.Models.ClientInformation;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommand : IRequest<CommomResponse>
    {
        public ClientModel ClientInformation { get; set; }
    }
}
