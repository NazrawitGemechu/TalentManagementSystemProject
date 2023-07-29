using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.JobQuery;
using TalentManagement.Application.ViewModels;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, Job>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetJobDetailQueryHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Job> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
        {
            //recives  query and id 
            //then retrives the job details form the job table and related tables
            var jobDetail = await _context.Jobs.Include(u => u.Company)
          .Include(s => s.Skills).ThenInclude(a => a.Skill)
          .Include(t => t.Recruter)
          .FirstOrDefaultAsync(n => n.Id == request.Id && n.IsAccepted != false);
           //returns the result back to the controller
             return jobDetail;
           


        }
    }
}
