using Barsa.Abstracts;
using Barsa.Interfaces;
using System.Net.Http;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ExecuteReceivedCommand : HandleClientExceptions
    {
        private HttpClient _httpClient;
        public ExecuteReceivedCommand(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {
            _httpClient = httpClient.CreateClient();
        }

        public void Execute()
        {

        }
    }
}
