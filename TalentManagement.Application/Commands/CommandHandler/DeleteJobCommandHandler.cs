using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.JobCommand;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteJobCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.Include(c => c.Jobs).ThenInclude(j => j.Skills)
             .FirstOrDefaultAsync(c => c.Id == request.JobId);

            if (company == null)
            {
                return false;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
