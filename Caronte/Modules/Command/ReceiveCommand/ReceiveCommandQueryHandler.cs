using CaronteLib.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ReceiveCommandQueryHandler : IRequestHandler<CommomMediatorResponses>
    {
        private HttpClient _httpClient;
        public ReceiveCommandQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<CommomMediatorResponses> Handle(ReceiveCommandQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _httpClient.GetAsync("");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
