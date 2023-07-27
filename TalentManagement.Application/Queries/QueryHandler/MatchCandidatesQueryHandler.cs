using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.JobQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class MatchCandidatesQueryHandler : IRequestHandler<MatchCandidatesQuery, List<UserJob>>
    {
        private readonly ApplicationDbContext _context;

        public MatchCandidatesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserJob>> Handle(MatchCandidatesQuery request, CancellationToken cancellationToken)
        {
            var jobSkills = _context.JobSkill.Where(x => x.JobId == request.JobId).Select(x => x.SkillId).ToList();

            var candidates = await _context.Candidates.Where(x => x.JobId == request.JobId)
                .Include(x => x.User)
                .Include(x => x.User.Talent)
                // Match based on skills
                .Where(x => x.User.Talent.Skills.Any(s => jobSkills.Contains(s.SkillId)))
                // Match based on company country
                .Where(x => x.User.Talent.Country == x.Job.Company.Country)
                // Match based on required years of experience
                .Where(x => x.User.Talent.TalentExperiences.Any(e => e.NumberOfYears >= x.Job.YearsOfExp))
                .ToListAsync(cancellationToken);

            return candidates;
        }
    }
}
