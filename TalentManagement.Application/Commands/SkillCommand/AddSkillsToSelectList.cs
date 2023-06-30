using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Commands.SkillCommand
{
    public class AddSkillsToSelectList : IRequest<Skill>
    {
        public Skill selectList { get; set; }

        public Skill BindSkills()
        {
            return BindSkills();
        }
        // public SelectListItem selectList { get; set; }

    }
}
