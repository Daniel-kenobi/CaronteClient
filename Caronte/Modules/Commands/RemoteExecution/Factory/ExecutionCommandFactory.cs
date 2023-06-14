using Barsa.Commons;
using Barsa.Modules.Interfaces;
using Caronte.Modules.Command.ReceiveCommand.Commands;
using System;

namespace Caronte.Modules.Commands.RemoteExecution.Factory
{
    public class ExecutionCommandFactory
    {
        private readonly CommandType _commandType;
        public ExecutionCommandFactory(CommandType commandType)
        {
            _commandType = commandType;
        }

        public IRemoteCommand CreateCommand()
        {
            switch(_commandType)
            {
                case CommandType.CMD:
                    return new CMD();

                case CommandType.DELETE_FILE:
                    return new DeleteFile();

                case CommandType.GET_FILE:
                    return new GetFile();
            }

            throw new NotImplementedException("Comando ainda não definido");
        }
    }
}
