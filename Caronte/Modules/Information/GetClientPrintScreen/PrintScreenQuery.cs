using Barsa.Commons;
using MediatR;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQuery : IRequest<CommonResponse>
    {
        public double SecondsToGetScreenshots { get; set; }
    }
}
