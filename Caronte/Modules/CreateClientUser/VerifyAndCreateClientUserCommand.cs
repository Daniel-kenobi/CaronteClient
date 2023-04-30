using Barsa.Models.User;
using Barsa.CommomResponses;
using MediatR;

namespace Caronte.Modules.CreateClientUser
{
    public class VerifyAndCreateClientUserCommand : IRequest<CommomMediatorResponse>
    {
        public UserModel CreateClientUserModel { get; set; }
    }
}
