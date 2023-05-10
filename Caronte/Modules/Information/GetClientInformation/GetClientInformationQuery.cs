using Barsa.Models.Client;
using Barsa.Commons;
using MediatR;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQuery : IRequest<CommonResponse<ClientModel>>
    {

    }
}
