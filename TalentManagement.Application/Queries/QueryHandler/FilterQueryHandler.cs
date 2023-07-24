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
    public class FilterQueryHandler : IRequestHandler<FilterQuery, List<Job>>
    {
        private readonly ApplicationDbContext _context;

        public FilterQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> Handle(FilterQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs.ToListAsync(cancellationToken);
            var companies = await _context.Companies.ToListAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                var filterResult = jobs.Where(n => n.JobTitle.Contains(request.SearchString) || n.JobType.Contains(request.SearchString)).ToList();
                return filterResult;
            }
            return jobs;
        }
    }
}
