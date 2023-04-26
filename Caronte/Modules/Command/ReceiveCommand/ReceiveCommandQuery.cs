﻿using Barsa.CommomResponses;
using MediatR;

namespace Caronte.Modules.Command.ReceiveCommand
{
    public class ReceiveCommandQuery : IRequest<CommomMediatorResponse>
    {
        public int Seconds { get; set; }
    }
}
