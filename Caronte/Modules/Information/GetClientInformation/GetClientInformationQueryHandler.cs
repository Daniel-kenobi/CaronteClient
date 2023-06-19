using Caronte.Domain.Models.Client;
using Caronte.Domain.Responses;
using Caronte.Utils.ApiUrl;
using Caronte.Utils.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.Information.GetClientInformation
{
    public class GetClientInformationQueryHandler : HandleExceptions, IRequestHandler<GetClientInformationQuery, CommonResponse<ClientModel>>
    {
        public GetClientInformationQueryHandler(IHttpClientFactory httpClientFactory, IWebServiceURL webServiceURLFactory) : base(httpClientFactory, webServiceURLFactory)
        {

        }

        public async Task<CommonResponse<ClientModel>> Handle(GetClientInformationQuery request, CancellationToken cancellationToken)
        {
            return new CommonResponse<ClientModel>(new ClientModel()
            {
                TimeZone = GetTimeZone(),
                ExternalIp = await GetExternalIp(),
                LocalIp = await GetLocalIp(),
                ClientName = await GetClientName(),
                ProcessorIdentifier = await GetProcessorIdentifer(),
                Osversion = GetOSVersion()
            });
        }

        private async Task<string> GetClientName()
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

        private string GetTimeZone() =>
            TimeZoneInfo.Local.ToString();

        private async Task<string> GetLocalIp()
        {
            string ip = "";

            try
            {
                var dns = await Dns.GetHostEntryAsync(Dns.GetHostName());
                ip = dns.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return ip;
        }

        private async Task<string> GetExternalIp()
        {
            string ipAddress = string.Empty;

            try
            {
                ipAddress = IPAddress.Parse((await new WebClient().DownloadStringTaskAsync("http://icanhazip.com")).Replace("\n", "").Replace("\"", "")).ToString();
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return ipAddress;
        }

        private async Task<string> GetProcessorIdentifer()
        {
            var hwid = string.Empty;

            try
            {
                var managementSearcher = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                var managementObjects = managementSearcher.Get();

                foreach (ManagementObject managementObject in managementObjects)
                    hwid = managementObject["ProcessorId"].ToString();
            }
            catch (Exception ex)
            {
                await Handle(ex);
            }

            return hwid;
        }

        private string GetOSVersion()
        {
            return Environment.OSVersion.VersionString;
        }
    }
}
