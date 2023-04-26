using Barsa.Models.ClientInformation;
using Barsa.CommomResponses;
using MediatR;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQuery : IRequest<CommomMediatorResponse<ClientInformation>>
    {

    }
}
