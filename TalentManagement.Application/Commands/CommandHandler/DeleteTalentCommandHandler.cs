using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteTalentCommandHandler : IRequestHandler<DeleteTalentCommand,bool>
    {
        private readonly ApplicationDbContext _context;
        public DeleteTalentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTalentCommand request, CancellationToken cancellationToken)
        {
            var talent = await _context.Talents.FindAsync(request.TalentId);
            if (talent == null)
                return false;

            _context.Talents.Remove(talent);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
