using Caronte.Domain.Models.Client;
using Caronte.Domain.Responses;
using MediatR;

namespace Caronte.Modules.Command.RemoteExecution
{
    public class RemoteExecutionCommand : IRequest<CommonResponse>
    {
        public ClientModel ClientModel { get; set; } = null!;
    }
}
