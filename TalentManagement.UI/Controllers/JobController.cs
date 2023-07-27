using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using TalentManagement.Application.Commands.JobCommand;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Application.Queries.CompanyQuery;
using TalentManagement.Application.Queries.JobQuery;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.Application.ViewModels;
using Azure.Core;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Linq;

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
        [Authorize(Roles = "Admin,Talent")]
        public async Task<IActionResult> Index()
        {
            var jobQuery = new GetJobsQuery();
            var companyQuery = new GetCompaniesQuery();
            var jobResult = await _mediator.Send(jobQuery);
            var companyResult = await _mediator.Send(companyQuery);
            return View(jobResult);
        }
        //search
        public async Task<IActionResult> Filter(string searchString)
        {
            var query = new FilterQuery { SearchString = searchString };
            var result = await _mediator.Send(query);
            return View("Index", result);
        }
        public async Task<IActionResult> FilterCandidate(string searchString)
        {
           
            var candidates = _context.Candidates.Include(u=>u.User).ThenInclude(t=>t.Talent).ThenInclude(s=>s.Skills).ToList();
            var company = _context.Companies.ToList();
            var talents = _context.Talents.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = candidates.Where(n =>n.User.Talent.FirstName.Contains(searchString) || n.User.Talent.Email.Contains(searchString) ).ToList();
                return View("Candidates", filterResult);
            }

            return View("Candidates", candidates);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {           
            string UserId = _userManager.GetUserId(User);
            ViewBag.IsApplied = _context.Candidates.Where(z => z.JobId == Id && z.UserId == UserId).FirstOrDefault();
            var query = new GetJobDetailQuery { Id = Id};
            var jobDetail = await _mediator.Send(query);
              return View(jobDetail);          
        }
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> YourPosts()
        { 
            var recruiterId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var jobs = await _mediator.Send(new YourPostsQuery { RecruiterId = recruiterId });
            var companies = await _mediator.Send(new GetCompaniesQuery());  
            return View(jobs);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
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
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> PostJob(PostAJobViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var command = new CreateJobCommand { Model = model };
                var result = await _mediator.Send(command);
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
            model.Skills = await BindSkills();
            model.EducationTypes = await Educations();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var query = new GetJobQuery { JobId = Id };
            var model = await _mediator.Send(query);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteJob(PostAJobViewModel model)
        {         
            var result = await _mediator.Send(new DeleteJobCommand { JobId = model.Id });
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction("YourPosts");
        }
        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> EditJob(int Id)
        {
            var query = new GetJobQuery { JobId = Id };
            var model = await _mediator.Send(query);
            //PostAJobViewModel model = new PostAJobViewModel();
            //List<int> skillsIds = new List<int>();
            // //Get job 
            //    var job = _context.Jobs.Include("Skills").FirstOrDefault(x => x.Id == Id);
            //    var company = _context.Companies.Include("Jobs").FirstOrDefault(x => x.Id == Id);
            //    //Get job skills and add each skillId into selectedskills list
            //    job.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));
            //    //bind model 
            //    model.Skills = _context.Skills.Select
            //        (x => new SelectListItem
            //        {
            //            Text = x.SkillName,
            //            Value = x.Id.ToString()
            //        }).ToList();
            //    model.Id = company.Id;
            //    model.JobTitle = job.JobTitle;
            //    model.JobDescription = job.JobDescription;
            //    model.JobDeadline = job.JobDeadline;
            //    model.JobType = job.JobType;
            //    model.PostedDate = job.PostedDate;
            //    model.Vacancy = job.Vacancy;
            //    model.Salary = job.Salary;
            //    model.YearsOfExp = job.YearsOfExp;
            //    model.Education = job.Education;
            //    model.SelectedSkills = skillsIds.ToArray();
            //    model.CompanyName = job.Company.CompanyName;
            //    model.CompanyEmail = job.Company.CompanyEmail;
            //    model.Country = job.Company.Country;

            //List<SelectListItem> listItems = new List<SelectListItem>();
            //listItems.Add(new SelectListItem()
            //{
            //    Value = "FullTime",
            //    Text = "FullTime"
            //});

            //listItems.Add(new SelectListItem()
            //{
            //    Value = "PartTime",
            //    Text = "PartTime"
            //});
            //model.JobTypes = listItems;
            //model.EducationTypes = await Educations();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> EditJob(PostAJobViewModel model)
        {
           // var dbjob = _context.Jobs.FirstOrDefault(n => n.Id == model.Id);

            if (ModelState.IsValid)
            {
                //var company = _context.Companies.Include(c => c.Jobs).ThenInclude(j => j.Skills).FirstOrDefault(c => c.Id == model.Id);
                //if (company == null)
                //{
                //    return NotFound();
                //}

                //_context.Companies.Remove(company);
                //_context.SaveChanges();

                ////then add the datas as new

                //Job job = new Job()
                //{
                //    JobTitle = model.JobTitle,
                //    JobDescription = model.JobDescription,
                //    JobDeadline = model.JobDeadline,
                //    JobType = model.JobType,
                //    PostedDate = model.PostedDate,
                //    Vacancy = model.Vacancy,
                //    Salary = model.Salary,
                //    YearsOfExp = model.YearsOfExp,
                //    Education = model.Education,
                //};
                //job.RecruterId = _userManager.GetUserId(User);
                //Company compan = new Company()
                //{
                //    CompanyName = model.CompanyName,
                //    CompanyEmail = model.CompanyEmail,
                //    Country = model.Country,
                //};

                //foreach (var item in model.SelectedSkills)
                //{
                //    job.Skills.Add(new JobSkill()
                //    {
                //        SkillId = item
                //    });
                //}
                //compan.Jobs.Add(job);
                //_context.Add(compan);
                //_context.SaveChanges();
                var command = new UpdateJobCommand { Model = model };
                var result = await _mediator.Send(command);

                return RedirectToAction("YourPosts");
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
        [HttpPost]
        [Authorize(Roles = "Talent")]
        public async Task<ActionResult> Apply(int _id)
        {
            string UserId = _userManager.GetUserId(User);

            var command = new ApplyForJobCommand { JobId = _id, UserId = UserId };
            var result = await _mediator.Send(command);
           return RedirectToAction("AppliedJobs");
        }
        [Authorize(Roles = "Talent")]
        public async Task<ActionResult> AppliedJobs(int id)
        {
                string UserId = _userManager.GetUserId(User);
                var pageOfResults = _context.Candidates.Where(x => x.UserId == UserId)
                                        .Include(x => x.Job.Company)
                                        .Include(x => x.Job)
                                        .Include(x => x.Job.Skills)
                                        .Include(x => x.Job.Recruter)
                                        .ToList();

                var count = _context.Candidates.Where(x => x.UserId == UserId).Count();                      
            return View(pageOfResults);
        }       
        [Authorize(Roles = "Company")]
        public async Task<ActionResult> Candidates(int id)
        {           
            string userId = _userManager.GetUserId(User);
            ViewBag.Job = _context.Jobs.Where(x => x.Id == id && x.RecruterId == userId).FirstOrDefault();
            if (ViewBag.Job == null)
            {
                return RedirectToAction("Home", "Main");
            }
            var query = new GetCandidatesQuery { JobId = id, UserId = userId };
            var candidates = await _mediator.Send(query);
            var count = candidates.Count();
            ViewBag.TotalCandidates = count;
            return View(candidates);                     
        }        
        public async Task<ActionResult> MatchCandidates(int id)
        {
            string userId = _userManager.GetUserId(User);
            ViewBag.Job = _context.Jobs.Where(x => x.Id == id && x.RecruterId == userId).FirstOrDefault();
            if (ViewBag.Job == null)
            {
                return RedirectToAction("Home", "Main");
            }

            var query = new MatchCandidatesQuery { JobId = id, UserId = userId };
            var candidates = await _mediator.Send(query);
            var count = candidates.Count();
            ViewBag.TotalCandidates = count;
            return View(candidates);
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
