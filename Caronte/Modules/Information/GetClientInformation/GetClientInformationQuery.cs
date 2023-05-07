using Barsa.Models.ClientInformation;
using Barsa.Commoms;
using MediatR;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQuery : IRequest<CommomResponse<ClientInformation>>
    {

    }
}
