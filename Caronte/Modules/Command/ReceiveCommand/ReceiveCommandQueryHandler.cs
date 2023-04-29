using Barsa.Abstracts;
using Barsa.Interfaces;
using Barsa.Models.Enums;
using Barsa.Models.Errors;
using Barsa.CommomResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ReceiveCommandQueryHandler : HandleClientExceptions, IRequestHandler<ReceiveCommandQuery, CommomMediatorResponse>
    {
        private HttpClient _httpClient;
        public ReceiveCommandQueryHandler(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {
            _httpClient = httpClient.CreateClient();
        }

        public async Task<CommomMediatorResponse> Handle(ReceiveCommandQuery request, CancellationToken cancellationToken)
        {
            var response = new CommomMediatorResponse();

            try
            {
                await _httpClient.GetAsync("");
            }
            catch (Exception ex)
            {
                await Handle(ex);
                response.AddErrors(new MediatorErrors(ErrorType.Unspecified, ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
