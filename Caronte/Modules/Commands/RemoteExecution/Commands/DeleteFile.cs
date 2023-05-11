using Barsa.Abstracts;
using Barsa.Commons;
using Barsa.Models.Errors;
using System;
using System.Collections.Generic;
using System.IO;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class DeleteFile : AbstractHandler
    {
        public override CommonResponse Handle(CommandType CommandType, object parameter)
        {
            if (CommandType != CommandType.DELETE_FILE)
                return base.Handle(CommandType, parameter);

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
