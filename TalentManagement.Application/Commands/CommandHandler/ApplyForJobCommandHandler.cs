using MediatR;
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
    public class ApplyForJobCommandHandler : IRequestHandler<ApplyForJobCommand, ActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ApplyForJobCommandHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<ActionResult> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
        {
            string UserId = request.UserId;

            Job job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId && x.IsAccepted == true);

            var talent = await _context.Talents.FirstOrDefaultAsync(x => x.ApplicantId == UserId );

            if (talent == null)
            {
                return new ViewResult { ViewName = "UploadResume" };
            }
           if(talent!=null && talent.IsAccepted!=true)
            {
                return new ViewResult { ViewName = "Pending" };
            }
            if (job == null)
                return new RedirectToActionResult("Home", "Main", null);

            UserJob applied = await _context.Candidates.FirstOrDefaultAsync(x => x.JobId == job.Id && x.UserId == UserId);

            if (applied == null)
            {
                UserJob user = new UserJob { UserId = UserId, JobId = job.Id };
                _context.Candidates.Add(user);
                await _context.SaveChangesAsync();
            }

            return new RedirectToActionResult("Detail", "Job", null);
           
        }
    }
}
