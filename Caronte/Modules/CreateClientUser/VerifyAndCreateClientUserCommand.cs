using Barsa.Models.User;
using Barsa.Commoms;
using MediatR;

namespace Caronte.Modules.CreateClientUser
{
    public class VerifyAndCreateClientUserCommand : IRequest<CommomResponse>
    {
        public UserModel CreateClientUserModel { get; set; }
    }
}
