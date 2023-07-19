using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public JobController(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            var job = _context.Jobs.ToList();
            var company = _context.Companies.ToList();
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
            var company = _context.Companies.FirstOrDefault(u => u.Id == Id);
            var jobDetail = _context.Jobs.Include(u => u.Company)
                .Include(s => s.Skills).ThenInclude(a => a.Skill)
                .FirstOrDefault(n => n.Id == Id);
            return View(jobDetail);
        }
        public async Task<IActionResult> YourPosts()
        {
            var company = _context.Companies.ToList();
            var model = await _context.Jobs
                                .Where(a => a.JobPosterId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                                .ToListAsync();
            return View(model);
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
            if (ModelState.IsValid)
            {
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
                };
                job.JobPosterId = _userManager.GetUserId(User);
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
                return View("YourPosts");
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
            model.Skills = await BindSkills();
            model.EducationTypes = await Educations();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            PostAJobViewModel model = new PostAJobViewModel();

            List<int> skillsIds = new List<int>();
            //Get job 
            var job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == Id);
            var company = _context.Companies.FirstOrDefault(x => x.Id == Id);
            //Get job skills and add each skillId into selectedskills list
            job.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));
            //bind model 
            model.Skills = _context.Skills.Select
                (x => new SelectListItem
                {
                    Text = x.SkillName,
                    Value = x.Id.ToString()
                }).ToList();

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


            company.CompanyName = model.CompanyName;
            company.CompanyEmail = model.CompanyEmail;
            company.Country = model.Country;
          
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteJob(PostAJobViewModel model)
        {

            Job job = new Job();
            List<JobSkill> jobSkills = new List<JobSkill>();


            job.JobTitle = model.JobTitle;
            job.JobDescription = model.JobDescription;
            job.JobDeadline = model.JobDeadline;
            job.JobType = model.JobType;
            job.PostedDate = model.PostedDate;
            job.Vacancy = model.Vacancy;
            job.Salary = model.Salary;
            job.YearsOfExp = model.YearsOfExp;
            job.Education = model.Education;

            job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == model.Id);
            job.Skills.ToList().ForEach(result => jobSkills.Add(result));
            _context.JobSkill.RemoveRange(jobSkills);
            _context.SaveChanges();
            _context.Attach(job);
            _context.Entry(job).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditJob(int Id)
        {
            PostAJobViewModel model = new PostAJobViewModel();

            List<int> skillsIds = new List<int>();
            //Get job 
            var job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == Id);
            var company = _context.Companies.FirstOrDefault(x => x.Id == Id);
            //Get job skills and add each skillId into selectedskills list
            job.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));
            //bind model 
            model.Skills = _context.Skills.Select
                (x => new SelectListItem
                {
                    Text = x.SkillName,
                    Value = x.Id.ToString()
                }).ToList();

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
            model.CompanyName = company.CompanyName;
            model.CompanyEmail = company.CompanyEmail;
            model.Country = company.Country;

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
            model.EducationTypes = await Educations();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditJob(PostAJobViewModel model)
        {
            var dbjob = _context.Jobs.FirstOrDefault(n => n.Id == model.Id);

            if (ModelState.IsValid)
            {

                dbjob.JobTitle = model.JobTitle;
                dbjob.JobDescription = model.JobDescription;
                dbjob.JobDeadline = model.JobDeadline;
                dbjob.JobType = model.JobType;
                dbjob.PostedDate = model.PostedDate;
                dbjob.Vacancy = model.Vacancy;
                dbjob.Salary = model.Salary;
                dbjob.YearsOfExp = model.YearsOfExp;
                dbjob.Education = model.Education;

                Company company = new Company();
                company.CompanyName = model.CompanyName;
                company.CompanyEmail = model.CompanyEmail;
                company.Country = model.Country;
                _context.SaveChanges();

                //remove exsting skills
                var existingSkillsDb= _context.JobSkill.Where(n=>n.JobId==model.Id).ToList();
                _context.JobSkill.RemoveRange(existingSkillsDb);
                _context.SaveChanges();
                Job job = new Job();
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
                return RedirectToAction("Index");
            }
            model.EducationTypes = await Educations();
            model.Skills = await BindSkills();
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
