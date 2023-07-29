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
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
       
        private readonly ICurrentUserService _currentUserService;

        public UpdateJobCommandHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {

            var model = request.Model;

            // Get job 
            var job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == model.Id);
            var company = _context.Companies.Include("Jobs").FirstOrDefault(x => x.Id == model.Id);

            // Update job properties
            job.JobTitle = model.JobTitle;
            job.JobDescription = model.JobDescription;
            job.JobDeadline = model.JobDeadline;
            job.JobType = model.JobType;
            job.PostedDate = model.PostedDate;
            job.Vacancy = model.Vacancy;
            job.Salary = model.Salary;
            job.YearsOfExp = model.YearsOfExp;
            job.Education = model.Education;
          

            // Update job skills
            var selectedSkills = new List<JobSkill>();
            if (model.SelectedSkills != null)
            {
                foreach (var skillId in model.SelectedSkills)
                {
                    selectedSkills.Add(new JobSkill { JobId = job.Id, SkillId = skillId });
                }
            }
            job.Skills = selectedSkills;

            // Update company properties
            company.CompanyName = model.CompanyName;
            company.CompanyEmail = model.CompanyEmail;
            company.Country = model.Country;

            // Save changes to database
            _context.Update(job);
            _context.Update(company);
            await _context.SaveChangesAsync();

            return Unit.Value;
          
        } 
    }
}
