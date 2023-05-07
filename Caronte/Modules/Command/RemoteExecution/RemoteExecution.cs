using Barsa.Abstracts;
using Barsa.Interfaces;
using System.Net.Http;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class RemoteExecution : HandleClientExceptions
    {
        public RemoteExecution(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {

        }

        public void Execute()
        {

        }
    }
}
