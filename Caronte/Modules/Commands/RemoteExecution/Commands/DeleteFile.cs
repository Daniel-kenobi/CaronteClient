using Barsa.Commons;
using Barsa.Models.Errors;
using Barsa.Modules.Interfaces;
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
                response.AddErrors(new Errors(ErrorType.Unspecified, ex?.InnerException?.Message ?? ex?.Message, new List<Exception>() { ex }));
            }

            return response;
        }
    }
}
