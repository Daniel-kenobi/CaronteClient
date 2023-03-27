using CaronteLib.Models.CreateClientUser;
using MediatR;

namespace Caronte.Modules.CreateClientUser
{
    public class CreateClientUserQuery : IRequest
    {
        public CreateClientUserModel CreateClientUserModel { get; set; }
    }
}
