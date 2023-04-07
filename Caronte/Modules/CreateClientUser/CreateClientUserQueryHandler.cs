using CaronteLib.Interfaces;
using MediatR;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.CreateClientUser
{
    public class CreateClientUserQueryHandler : IRequestHandler<CreateClientUserQuery>
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceData _webServiceData;
        public CreateClientUserQueryHandler(IHttpClientFactory httpClientFactory, IWebServiceData webService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webServiceData = webService;
        }

        public async Task<Unit> Handle(CreateClientUserQuery request, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(request.CreateClientUserModel);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_webServiceData.CreateClientUserEndpoint(), stringContent);

            return Unit.Value;
        }
    }
}
