﻿using Caronte.Domain.Interfaces;
using Caronte.Domain.Responses;
using System;
using System.IO;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class GetFile : IRemoteCommand
    {
        public object Execute(object parameter)
        {
            var response = new CommonResponse();

            var filePath = Convert.ToString(parameter);
            var fileByte = File.ReadAllBytes(filePath);

            return response;
        }
    }
}
