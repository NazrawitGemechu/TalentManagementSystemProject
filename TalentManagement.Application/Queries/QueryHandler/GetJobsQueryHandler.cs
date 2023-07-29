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
    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, List<Job>>
    {
        private readonly ApplicationDbContext _context;

        public GetJobsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Job>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            //from the job controller it recives the get jobs guery then retrives it from the database
            // var jobs = await _context.Jobs.ToListAsync(cancellationToken);
            var jobs=_context.Jobs.Where(x => x.IsAccepted == true).OrderByDescending(z => z.PostedDate).
                 Include(y => y.Recruter).
                 ToList();
            //returns the result stored the variable back to the controller
            return jobs;
        }
    }
}
