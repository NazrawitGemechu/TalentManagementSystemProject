using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetTalentsByApplicantIdQueryHandler : IRequestHandler<GetTalentsByApplicantIdQuery, List<Talent>>
    {
        private readonly ApplicationDbContext _context;

        public GetTalentsByApplicantIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Talent>> Handle(GetTalentsByApplicantIdQuery request, CancellationToken cancellationToken)
        {
            var talents = await _context.Talents.Where(x => x.IsAccepted == true)
                                 .Where(a => a.ApplicantId == request.ApplicantId)
                                 .ToListAsync();

            return talents;
        }
    }
}
