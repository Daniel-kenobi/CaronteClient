using Barsa.Commoms;
using MediatR;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQuery : IRequest<CommomResponse>
    {
        public double SecondsToGetScreenshots { get; set; }
    }
}
