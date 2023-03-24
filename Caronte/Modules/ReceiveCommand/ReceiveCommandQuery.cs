using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caronte.Modules.ReceiveCommand
{
    public class ReceiveCommandQuery : IRequest
    {
        public int Seconds { get; set; }
    }
}
