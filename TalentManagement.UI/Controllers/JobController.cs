using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Models.ViewModels;

namespace TalentManagement.UI.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public JobController(ApplicationDbContext context, IMediator mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        public IActionResult Index()
        {
            var job= _context.Jobs.ToList();
            var company= _context.Companies.ToList();
            //DisplayJobViewModel vm = new DisplayJobViewModel();
              
            return View(job);
        }
        //search
        public async Task<IActionResult> Filter(string searchString)
        {

            var job = _context.Jobs.ToList();
            var company = _context.Companies.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = job.Where(n => n.JobTitle.Contains(searchString) || n.JobType.Contains(searchString)).ToList();
                return View("Index", filterResult);
            }
            return View("Index", job);
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int Id)
        {
            var job = _context.Jobs.FirstOrDefault(u => u.Id == Id);
            var company = _context.Companies.FirstOrDefault(u=>u.Id==Id);
            var jobDetail= _context.Jobs.Include(u=>u.Company)
                .Include(s=>s.Skills).ThenInclude(a=>a.Skill)
                .FirstOrDefault(n=>n.Id==Id);



           


            return View(jobDetail);
        }


        [HttpGet]
        public async Task<IActionResult> PostJob()
        {
            PostAJobViewModel vm = new PostAJobViewModel();
            vm.Skills = await BindSkills();
            vm.EducationTypes = await Educations();
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "FullTime",
                Text = "FullTime"
            });
           
            listItems.Add(new SelectListItem()
            {
                Value = "PartTime",
                Text = "PartTime"
            });
            vm.JobTypes = listItems;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PostJob(PostAJobViewModel model)
        {
            if(ModelState.IsValid)
            {
                Job job = new Job()
                {
                 JobTitle = model.JobTitle,
                 JobDescription = model.JobDescription,
                 JobDeadline = model.JobDeadline,
                 JobType = model.JobType,
                 PostedDate = model.PostedDate,
                 Vacancy=model.Vacancy,
                 Salary=model.Salary,
                 YearsOfExp=model.YearsOfExp,
                 Education=model.Education,
                };
                Company company = new Company()
                {
                    CompanyName = model.CompanyName,
                    CompanyEmail = model.CompanyEmail,
                    Country = model.Country,
                    

                };
            
                foreach (var item in model.SelectedSkills)
                {
                    job.Skills.Add(new JobSkill()
                    {
                        SkillId = item
                    });
                }
                company.Jobs.Add(job);
                 _context.Add(company);
                _context.SaveChanges();
                return View("RegisterComplete");
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "FullTime",
                Text = "FullTime"
            });

            listItems.Add(new SelectListItem()
            {
                Value = "PartTime",
                Text = "PartTime"
            });
            model.JobTypes = listItems;
            model.Skills= await BindSkills();
            model.EducationTypes = await Educations();
            return View(model);
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
