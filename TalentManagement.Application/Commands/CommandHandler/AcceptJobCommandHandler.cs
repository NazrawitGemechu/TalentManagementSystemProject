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
    public class AcceptJobCommandHandler : IRequestHandler<AcceptJobCommand,Unit>
    {
        private readonly ApplicationDbContext _context;

        public AcceptJobCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AcceptJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);

            if (job != null)
            {
                job.IsAccepted = true;
                await _context.SaveChangesAsync();
            }
           

            return Unit.Value;
        }

        
    }

}
