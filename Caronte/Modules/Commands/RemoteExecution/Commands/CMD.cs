using Barsa.Commons;
using Barsa.Interfaces;
using Barsa.Models.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class CMD : IRemoteCommand
    {
        public object Execute(object parameter)
        {
            CommonResponse response = new();

            try
            {
                var processStartInfo = new ProcessStartInfo("cmd.exe", $"/C {Convert.ToString(parameter)}");

                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                using (var processo = new Process())
                {
                    processo.StartInfo = processStartInfo;
                    processo.Start();
                }
            }
            catch (Exception ex)
            {
                response.AddErrors(new Errors(ErrorType.Unspecified, ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
