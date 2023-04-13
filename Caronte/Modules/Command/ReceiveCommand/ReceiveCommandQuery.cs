using MediatR;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ReceiveCommandQuery : IRequest<CommomMediatorResponses>
    {
        public int Seconds { get; set; }
    }
}
