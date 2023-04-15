using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.CMD.ExecuteCMDCommand
{
    public class ExecuteCmdCommand : IRequest<CommomMediatorResponse>
    {
        public string Command { get; set; }
    }
}
