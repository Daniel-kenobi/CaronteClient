using Caronte.Domain.Enums;
using Caronte.Domain.Interfaces;
using Caronte.Domain.Models.Errors;
using Caronte.Domain.Responses;
using System;
using System.Collections.Generic;
using System.IO;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class DeleteFile : IRemoteCommand
    {
        public object Execute(object parameter)
        {
            var response = new CommonResponse();

            try
            {
                File.Delete(Convert.ToString(parameter));
            }
            catch (Exception ex)
            {
                response.AddErrors(new Error(ErrorTypeEnum.Unspecified, ex?.InnerException?.Message ?? ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
