using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, List<Skill>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllSkillsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Skills.ToListAsync();
        }
    }
}
