using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, Skill>
    {
        private readonly ApplicationDbContext _context;

        public CreateSkillHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Skill> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            _context.Skills.Add(request.NewSkill);
            await _context.SaveChangesAsync();
            return request.NewSkill;
        }
    }
}
