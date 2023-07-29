using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetTalentDetailQueryHandler : IRequestHandler<GetTalentDetailQuery, Talent>
    {
        private readonly ApplicationDbContext _context;

        public GetTalentDetailQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Talent> Handle(GetTalentDetailQuery request, CancellationToken cancellationToken)
        {
            var talentDetail = await _context.Talents
            .Include(u => u.TalentExperiences)
            .Include(s => s.Skills).ThenInclude(a => a.Skill)
            .Include(s => s.EducationLevels).ThenInclude(a => a.EducationLevel)
            .FirstOrDefaultAsync(n => n.Id == request.Id && n.IsAccepted != false);
            return talentDetail;
        }
    }
}