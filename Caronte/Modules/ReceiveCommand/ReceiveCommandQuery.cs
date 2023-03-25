using MediatR;

namespace Caronte.Modules.ReceiveCommand
{
    public class ReceiveCommandQuery : IRequest
    {
        public int Seconds { get; set; }
    }
}
