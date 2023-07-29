using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.AdminCommand;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class AcceptTalentCommandHandler : IRequestHandler<AcceptTalentCommand,Unit>
    {
        private readonly ApplicationDbContext _context;

        public AcceptTalentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AcceptTalentCommand request, CancellationToken cancellationToken)
        {
            var talent = await _context.Talents.FirstOrDefaultAsync(j => j.Id == request.TalentId);

            if (talent != null)
            {
                talent.IsAccepted = true;
                await _context.SaveChangesAsync();
            }
            

            return Unit.Value;
        }
    }
}
