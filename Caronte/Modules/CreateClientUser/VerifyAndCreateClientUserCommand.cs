using CaronteLib.Models.CreateClientUser;
using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.CreateClientUser
{
    public class VerifyAndCreateClientUserCommand : IRequest<CommomMediatorResponse>
    {
        public CreateClientUserModel CreateClientUserModel { get; set; }
    }
}
