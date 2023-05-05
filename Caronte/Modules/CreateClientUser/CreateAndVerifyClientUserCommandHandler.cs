using Barsa.Interfaces;
using Barsa.Models.Enums;
using Barsa.Models.Errors;
using Barsa.Commoms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.CreateClientUser
{
    public class CreateAndVerifyClientUserCommandHandler : IRequestHandler<VerifyAndCreateClientUserCommand, CommomResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceURLFactory _webServiceUrl;
        public CreateAndVerifyClientUserCommandHandler(IHttpClientFactory httpClientFactory, IWebServiceURLFactory webServiceUrl)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webServiceUrl = webServiceUrl;
        }

        public async Task<CommomResponse> Handle(VerifyAndCreateClientUserCommand request, CancellationToken cancellationToken)
        {
            var response = new CommomResponse();
            try
            {
                var json = JsonSerializer.Serialize(request.CreateClientUserModel);
                var httpResponse = await _httpClient.PostAsync(_webServiceUrl.CreateClientUser(), CreateStringContentToPost(json));

                if (!httpResponse.IsSuccessStatusCode)
                    response.AddErrors(new MediatorErrors(ErrorType.BadRequest, httpResponse.ReasonPhrase));
            }
            catch (Exception ex)
            {
                response.AddErrors(new MediatorErrors(ErrorType.BadRequest, ex.Message, new List<Exception>() { ex }));
            }

            return response;
        }

        private StringContent CreateStringContentToPost(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
