using Barsa.Commons;
using Barsa.Modules.Interfaces;
using Barsa.Models.Errors;
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
                response.AddErrors(new Error(ErrorType.Unspecified, ex?.InnerException?.Message ?? ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
