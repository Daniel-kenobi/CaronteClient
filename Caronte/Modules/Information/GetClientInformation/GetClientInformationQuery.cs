using CaronteLib.Models.ClientInformation;
using CaronteLib.Response;
using MediatR;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQuery : IRequest<CommomMediatorResponses<ClientInformation>>
    {

    }
}
