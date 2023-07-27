using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class AppliedJobsQueryHandler : IRequestHandler<AppliedJobsQuery, List<UserJob>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AppliedJobsQueryHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
           
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<UserJob>> Handle(AppliedJobsQuery request, CancellationToken cancellationToken)
        {
            string UserId = await _currentUserService.GetCurrentUserId();
            var pageOfResults = _context.Candidates.Where(x => x.UserId == UserId)
                .Include(x => x.Job.Company)
                .Include(x => x.Job)
                .Include(x => x.Job.Skills)
                .Include(x => x.Job.Recruter)
                .ToList();

            var count = _context.Candidates.Where(x => x.UserId == UserId).Count();

            return pageOfResults;
        }
    }
}
