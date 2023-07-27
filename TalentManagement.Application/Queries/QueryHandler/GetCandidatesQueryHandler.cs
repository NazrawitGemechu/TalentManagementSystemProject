using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, List<UserJob>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetCandidatesQueryHandler( ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _context = context;
        }
        public async Task<List<UserJob>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            //string userId = await _currentUserService.GetCurrentUserId();
            //var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId && x.RecruterId == userId);

           

            var candidates = await _context.Candidates
                .Where(x => x.JobId == request.JobId)
                .Include(x => x.User)
                .Include(x => x.User.Talent)
                .ToListAsync();

            return candidates;
        }
    }
}
