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
    public class YourPostsQueryHandler : IRequestHandler<YourPostsQuery, List<Job>>
    {
        private readonly ApplicationDbContext _context;

        public YourPostsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> Handle(YourPostsQuery request, CancellationToken cancellationToken)
        {
            //gets the yourpostsquery along with the reqruter id
            //retrives the jobs where the recruter id is equal to the retrived reqruter id
            var model = await _context.Jobs
          .Where(a => a.RecruterId == request.RecruiterId)
          .ToListAsync();
            //returns the result back to the controller
            return model;
        }
    }
}
