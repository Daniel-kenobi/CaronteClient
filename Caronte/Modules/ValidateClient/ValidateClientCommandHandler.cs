using Barsa.Interfaces;
using Barsa.Models.Errors;
using Barsa.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caronte.Modules.ValidateClient
{
    public class ValidateClientCommandHandler : IRequestHandler<ValidateClientCommand, CommonResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceURLFactory _webServiceUrl;

        public ValidateClientCommandHandler(IHttpClientFactory httpClientFactory, IWebServiceURLFactory webServiceUrl)
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
                var httpResponse = await _httpClient.PostAsync(_webServiceUrl.ValidateClient(), CreateStringContentToPost(json));

                if (!httpResponse.IsSuccessStatusCode)
                    response.AddErrors(new Errors(ErrorType.BadRequest, httpResponse.ReasonPhrase));
            }
            catch (Exception ex)
            {
                response.AddErrors(new Errors(ErrorType.BadRequest, ex.Message, new List<Exception>() { ex }));
            }

            return response;
        }

        private StringContent CreateStringContentToPost(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
