using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.JobCommand;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, IActionResult>
    {
        private readonly ApplicationDbContext _context;
       
        private readonly ICurrentUserService _currentUserService;

        public UpdateJobCommandHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var dbjob = _context.Jobs.FirstOrDefault(n => n.Id == model.Id);
            var company = _context.Companies.Include(c => c.Jobs).ThenInclude(j => j.Skills).FirstOrDefault(c => c.Id == model.Id);

            if (company == null)
            {
                return new NotFoundResult();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            //then add the data as new
            Job job = new Job()
            {
                JobTitle = model.JobTitle,
                JobDescription = model.JobDescription,
                JobDeadline = model.JobDeadline,
                JobType = model.JobType,
                PostedDate = model.PostedDate,
                Vacancy = model.Vacancy,
                Salary = model.Salary,
                YearsOfExp = model.YearsOfExp,
                Education = model.Education,
                RecruterId = await _currentUserService.GetCurrentUserId()
            };

            Company compan = new Company()
            {
                CompanyName = model.CompanyName,
                CompanyEmail = model.CompanyEmail,
                Country = model.Country
            };

            foreach (var item in model.SelectedSkills)
            {
                job.Skills.Add(new JobSkill()
                {
                    SkillId = item
                });
            }

            compan.Jobs.Add(job);
            _context.Add(compan);
            await _context.SaveChangesAsync();

            return new RedirectToActionResult("YourPosts", null, null);
        }
    }
}
