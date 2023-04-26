using Barsa.Models.CreateClientUser;
using Barsa.CommomResponses;
using MediatR;

namespace Caronte.Modules.CreateClientUser
{
    public class VerifyAndCreateClientUserCommand : IRequest<CommomMediatorResponse>
    {
        public ClientUserModel CreateClientUserModel { get; set; }
    }
}
