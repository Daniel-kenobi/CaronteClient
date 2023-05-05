using Barsa.Abstracts;
using Barsa.Interfaces;
using System.Net.Http;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ReceiveCommand : HandleClientExceptions
    {
        private HttpClient _httpClient;
        public ReceiveCommand(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {
            _httpClient = httpClient.CreateClient();
        }

        public void Receive()
        {

        }
    }
}
