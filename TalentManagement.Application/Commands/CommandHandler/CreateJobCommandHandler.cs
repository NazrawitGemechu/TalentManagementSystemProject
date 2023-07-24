using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.JobCommand;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, IActionResult>
    {
       // private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        public CreateJobCommandHandler(ICurrentUserService currentUserService, IMediator mediator,ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            model.Skills = await BindSkills();
            model.EducationTypes = await Educations();
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
        public async Task<List<SelectListItem>> BindSkills()
        {

            var skillsFromDb = _mediator.Send(new GetAllSkillsQuery());

            var selectList = new List<SelectListItem>();

            foreach (var item in await skillsFromDb)
            {

                selectList.Add(new SelectListItem(item.SkillName, item.Id.ToString()));
            }

            return selectList;
        }
        public async Task<List<SelectListItem>> Educations()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Software Engineering",
                Text = "Software Engineering"
            });

            listItems.Add(new SelectListItem()
            {
                Value = "Data Science",
                Text = "Data Science"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Computer Science",
                Text = "Computer Science"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Database Adminstrator",
                Text = "Database Adminstrator"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Web Developer",
                Text = "Web Developer"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Network Security",
                Text = "Network Security"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "Information System",
                Text = "Information System"
            });
            return listItems;
        }
    }
}
