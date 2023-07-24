using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.CompanyQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<Company>>
    {
        private readonly ApplicationDbContext _context;

        public GetCompaniesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Company>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _context.Companies.ToListAsync(cancellationToken);
            return companies;
        }
    }
}
