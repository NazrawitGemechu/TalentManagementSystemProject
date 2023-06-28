using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.EducationLevelCommand;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class CreateEducationLevelHandler : IRequestHandler<CreateEducationLevelCommand, EducationLevel>
    {
        private readonly ApplicationDbContext _context;

        public CreateEducationLevelHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EducationLevel> Handle(CreateEducationLevelCommand request, CancellationToken cancellationToken)
        {
            _context.EducationLevels.Add(request.NewEducationLevel);
            await _context.SaveChangesAsync();
            return request.NewEducationLevel;
        }
    }
}
