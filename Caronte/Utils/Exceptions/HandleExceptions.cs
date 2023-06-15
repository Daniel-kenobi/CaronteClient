using Caronte.Utils.ApiUrl;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Caronte.Utils.Exceptions
{
    public abstract class HandleExceptions
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceURL _urlFactory;

        public HandleExceptions(IHttpClientFactory httpClient, IWebServiceURL urlFactory)
        {
            _httpClient = httpClient.CreateClient();
            _urlFactory = urlFactory;
        }

        public async Task Handle(Exception ex)
        {
            await _httpClient.GetAsync(_urlFactory.GetWebServiceUrl());
        }
    }
}
