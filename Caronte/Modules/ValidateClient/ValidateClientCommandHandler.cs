using Caronte.Domain.Models.Errors;
using Caronte.Domain.Responses;
using Caronte.Utils.ApiUrl;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommandHandler : IRequestHandler<ValidateClientCommand, CommonResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceURL _webServiceUrl;

        public ValidateClientCommandHandler(IHttpClientFactory httpClientFactory, IWebServiceURL webServiceUrl)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webServiceUrl = webServiceUrl;
        }

        public async Task<CommonResponse> Handle(ValidateClientCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponse();

            try
            {
                var json = JsonSerializer.Serialize(request.ClientInformation);
                var httpResponse = await _httpClient.PostAsync(_webServiceUrl.ValidateClient(), CreateStringContentToPost(json), cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                    response.AddErrors(new Error(ErrorType.BadRequest, httpResponse?.ReasonPhrase ?? "Unspecified error"));
            }
            catch (Exception ex)
            {
                response.AddErrors(new Error(ErrorType.BadRequest, ex.Message, new List<Exception>() { ex }));
            }

            return response;
        }

        private StringContent CreateStringContentToPost(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
