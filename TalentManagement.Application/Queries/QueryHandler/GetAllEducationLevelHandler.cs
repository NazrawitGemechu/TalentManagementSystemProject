using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetAllEducationLevelHandler : IRequestHandler<GetAllEducationLevelsQuery, List<EducationLevel>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllEducationLevelHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EducationLevel>> Handle(GetAllEducationLevelsQuery request, CancellationToken cancellationToken)
        {
            return await _context.EducationLevels.ToListAsync();
        }
    }
}
