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
    public class RejectJobCommandHandler : IRequestHandler<RejectJobCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public RejectJobCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RejectJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);

            if (job != null)
            {
                job.IsAccepted = false;
                await _context.SaveChangesAsync();
            }
            

            return Unit.Value;
        }
    }

}
