using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.AdminQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class PendingPostsQueryHandler : IRequestHandler<PendingPostsQuery, List<Job>>
    {
        private readonly ApplicationDbContext _context;

        public PendingPostsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Job>> Handle(PendingPostsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs.Where(x => x.IsAccepted == null)
              .Include(x => x.Recruter)
              .ToListAsync();

            return jobs;
        }
    }
}
