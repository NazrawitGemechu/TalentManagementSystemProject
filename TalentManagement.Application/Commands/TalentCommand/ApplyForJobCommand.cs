using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Application.Commands.TalentCommand
{
    public class ApplyForJobCommand : IRequest<ActionResult>
    {
        public int JobId { get; set; }
        public string UserId { get; set; }
    }

}
