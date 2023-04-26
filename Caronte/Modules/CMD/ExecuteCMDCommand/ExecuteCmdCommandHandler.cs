using CaronteLib.Abstracts;
using CaronteLib.Interfaces;
using CaronteLib.Models.Enums;
using CaronteLib.Models.Errors;
using CaronteLib.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.CMD.ExecuteCMDCommand
{
    public class ExecuteCmdCommandHandler : SendErrorToServer, IRequestHandler<ExecuteCmdCommand, CommomMediatorResponse>
    {
        public ExecuteCmdCommandHandler(IHttpClientFactory httpClient, IWebServiceURLFactory urlFactory) : base(httpClient, urlFactory)
        {

        }

        public async Task<CommomMediatorResponse> Handle(ExecuteCmdCommand request, CancellationToken cancellationToken)
        {
            CommomMediatorResponse response = new();

            try
            {
                var commands = await GetCommandsFromServer();

                if (commands?.Count <= 0)
                    return response;

                var processStartInfo = new ProcessStartInfo("cmd.exe", $"/C {string.Join(" & ", commands)}");
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                using (Process processo = new Process())
                {
                    processo.StartInfo = processStartInfo;
                    processo.Start();
                }
            }
            catch (Exception ex)
            {
                await SendError(ex);
                response.AddErrors(new MediatorErrors(ErrorType.Unspecified, ex?.Message, new List<Exception>() { ex }));
            }

            return await Task.FromResult(response);
        }


        private async Task<List<string>> GetCommandsFromServer()
        {
            return new List<string>();
        }
    }
}
