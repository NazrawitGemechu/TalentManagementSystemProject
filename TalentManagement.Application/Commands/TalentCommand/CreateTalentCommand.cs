using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Commands.TalentCommand
{
    public class CreateTalentCommand : IRequest<Talent>
    {
        public Talent NewTalent { get; set; }
    }
}
