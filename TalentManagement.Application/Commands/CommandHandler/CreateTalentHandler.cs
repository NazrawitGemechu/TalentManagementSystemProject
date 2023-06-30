using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class CreateTalentHandler : IRequestHandler<CreateTalentCommand, Talent>
    {
        private readonly ApplicationDbContext _context;

        public CreateTalentHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Talent> Handle(CreateTalentCommand request, CancellationToken cancellationToken)
        {
            _context.Add(request.NewTalent);
            await _context.SaveChangesAsync();
            return request.NewTalent;
        }
    }
}
