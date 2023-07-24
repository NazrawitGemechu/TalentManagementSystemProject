using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.JobQuery;
using TalentManagement.Application.ViewModels;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, PostAJobViewModel>
    {
        private readonly ApplicationDbContext _context;

        public GetJobQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostAJobViewModel> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            PostAJobViewModel model = new PostAJobViewModel();

            List<int> skillsIds = new List<int>();

            // Get job
            var job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == request.JobId);
            var company = _context.Companies.Include("Jobs").FirstOrDefault(x => x.Id == request.JobId);

            // Get job skills and add each skillId into selectedskills list
            job.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));

            // Bind model
            model.Skills = _context.Skills.Select(x => new SelectListItem
            {
                Text = x.SkillName,
                Value = x.Id.ToString()
            }).ToList();

            model.Id = company.Id;
            model.JobTitle = job.JobTitle;
            model.JobDescription = job.JobDescription;
            model.JobDeadline = job.JobDeadline;
            model.JobType = job.JobType;
            model.PostedDate = job.PostedDate;
            model.Vacancy = job.Vacancy;
            model.Salary = job.Salary;
            model.YearsOfExp = job.YearsOfExp;
            model.Education = job.Education;
            model.SelectedSkills = skillsIds.ToArray();
            model.CompanyName = job.Company.CompanyName;
            model.CompanyEmail = job.Company.CompanyEmail;
            model.Country = job.Company.Country;

            return model;
        }
    }
}
