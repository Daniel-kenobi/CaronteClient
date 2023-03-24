using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.ReceiveCommand
{
    public class ReceiveCommandQueryHandler : IRequestHandler<ReceiveCommandQuery>
    {
        private HttpClient _httpClient;
        private System.Timers.Timer timeToGetRequest;
        public ReceiveCommandQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Unit> Handle(ReceiveCommandQuery request, CancellationToken cancellationToken)
        {
            timeToGetRequest = new(request.Seconds * 1000);
            timeToGetRequest.Elapsed += GetCommandOfApi;
            timeToGetRequest.Start();

            return Unit.Value;
        }

        private async void GetCommandOfApi(Object source, System.Timers.ElapsedEventArgs e)
        {
            timeToGetRequest.Stop();
            await _httpClient.GetAsync("");

        }
    }
}
