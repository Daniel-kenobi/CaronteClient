using Barsa.Commons;
using Barsa.Models.Client;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Caronte.Modules.Command.RemoteExecution
{
    public class RemoteExecutionCommand : IRequest<CommonResponse>
    {
        public ClientModel ClientModel { get; set; } = null!;
    }
}
