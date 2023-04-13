using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.CMD.ExecuteCMDCommand
{
    public class ExecuteCmdCommand : IRequest<CommomMediatorResponses>
    {
        public string Command { get; set; }
    }
}
