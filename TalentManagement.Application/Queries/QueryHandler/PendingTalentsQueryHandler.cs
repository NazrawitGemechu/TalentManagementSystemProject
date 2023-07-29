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
    public class PendingTalentsQueryHandler : IRequestHandler<PendingTalentsQuery, List<Talent>>
    {
        private readonly ApplicationDbContext _context;

        public PendingTalentsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Talent>> Handle(PendingTalentsQuery request, CancellationToken cancellationToken)
        {
            var talents = await _context.Talents.Where(x => x.IsAccepted == null)
            .Include(x => x.Applicant)
            .ToListAsync();

            return talents;
        }
    }
}
