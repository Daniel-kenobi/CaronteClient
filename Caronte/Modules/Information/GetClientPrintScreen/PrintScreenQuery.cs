using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.Information.GetClientPrintScreen
{
    public class PrintScreenQuery : IRequest<CommomMediatorResponses>
    {
        public double SecondsToGetScreenshots { get; set; }
    }
}
