using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQuery : IRequest<CommomMediatorResponse>
    {
        public double SecondsToGetScreenshots { get; set; }
    }
}
