using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.JobCommand;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class GetAppliedJobsQueryHandler : IRequestHandler<GetAppliedJobsQuery, List<Job>>
    {
        private readonly ApplicationDbContext _context;

        public GetAppliedJobsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Job>> Handle(GetAppliedJobsQuery request, CancellationToken cancellationToken)
        {
            var pageOfResults = await _context.Candidates.Where(x => x.UserId == request.UserId && x.JobId == request.Id)
                                    .Include(x => x.Job.Company)
                                    .Include(x => x.Job)
                                    .Include(x => x.Job.Skills)
                                    .Include(x => x.Job.Recruter)
                                    .Select(x => x.Job)
                                    .ToListAsync(cancellationToken);

            return pageOfResults;
        }
    }
}
