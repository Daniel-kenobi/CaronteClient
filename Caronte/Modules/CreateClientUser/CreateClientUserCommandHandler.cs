﻿using CaronteLib.Interfaces;
using CaronteLib.Models.Enums;
using CaronteLib.Models.Errors;
using CaronteLib.Response;
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
    public class CreateClientUserCommandHandler : IRequestHandler<CreateClientUserCommand, CommomMediatorResponses>
    {
        private readonly HttpClient _httpClient;
        private readonly IWebServiceURLFactory _webServiceUrl;
        public CreateClientUserCommandHandler(IHttpClientFactory httpClientFactory, IWebServiceURLFactory webServiceUrl)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webServiceUrl = webServiceUrl;
        }

        public async Task<CommomMediatorResponses> Handle(CreateClientUserCommand request, CancellationToken cancellationToken)
        {
            var response = new CommomMediatorResponses();
            try
            {
                var json = JsonSerializer.Serialize(request.CreateClientUserModel);
                var httpResponse = await _httpClient.PostAsync(_webServiceUrl.CreateClientUserEndpoint(), CreateStringContentToPost(json));

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
