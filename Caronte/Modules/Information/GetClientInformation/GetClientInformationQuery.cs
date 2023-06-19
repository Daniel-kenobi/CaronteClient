using Caronte.Domain.Models.Client;
using Caronte.Domain.Responses;
using MediatR;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQuery : IRequest<CommonResponse<ClientModel>>
    {

    }
}
