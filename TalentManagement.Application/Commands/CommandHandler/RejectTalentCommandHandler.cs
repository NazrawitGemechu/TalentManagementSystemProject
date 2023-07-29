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
    public class RejectTalentCommandHandler : IRequestHandler<RejectTalentCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public RejectTalentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RejectTalentCommand request, CancellationToken cancellationToken)
        {
            var talent = await _context.Talents.FirstOrDefaultAsync(j => j.Id == request.TalentId, cancellationToken);

            if (talent != null)
            {
                talent.IsAccepted = false;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }

}
