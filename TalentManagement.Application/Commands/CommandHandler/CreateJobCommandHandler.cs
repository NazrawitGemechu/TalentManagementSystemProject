using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, IActionResult>
    {
       // private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreateJobCommandHandler(ICurrentUserService currentUserService, ApplicationDbContext context)
        {
          
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var job = new Job
            {
                JobTitle = request.Model.JobTitle,
                JobDescription = request.Model.JobDescription,
                JobDeadline = request.Model.JobDeadline,
                JobType = request.Model.JobType,
                PostedDate = request.Model.PostedDate,
                Vacancy = request.Model.Vacancy,
                Salary = request.Model.Salary,
                YearsOfExp = request.Model.YearsOfExp,
                Education = request.Model.Education
            };

            job.RecruterId = await _currentUserService.GetCurrentUserId();

            var company = new Company
            {
                CompanyName = request.Model.CompanyName,
                CompanyEmail = request.Model.CompanyEmail,
                Country = request.Model.Country
            };

            foreach (var item in request.Model.SelectedSkills)
            {
                job.Skills.Add(new JobSkill { SkillId = item });
            }

            company.Jobs.Add(job);

            _context.Add(company);
            await _context.SaveChangesAsync();
            return new OkResult();
            // return new ViewResult { ViewName = "RegisterComplete" };
        }
    }
}
