using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Application.Commands.AdminCommand
{
    public class RejectJobCommand : IRequest<Unit>
    {
        public int JobId { get; set; }
    }
}
