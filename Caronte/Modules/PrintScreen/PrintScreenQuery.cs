using MediatR;

namespace Caronte.Modules.PrintScreen
{
    public class PrintScreenQuery : IRequest
    {
        public double SecondsToGetScreenshots { get; set; }
    }
}
