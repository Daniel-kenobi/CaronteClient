using Barsa.Models.User;
using Barsa.Commoms;
using MediatR;
using Barsa.Models.ClientInformation;

namespace Caronte.Modules.CreateClientUser
{
    public class VerifyAndCreateClientUserCommand : IRequest<CommomResponse>
    {
        public ClientInformation ClientInformation { get; set; }
    }
}
