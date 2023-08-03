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
            //assigns the get jobs query to vat jobQuery
            var jobQuery = new GetJobsQuery();
            //assigns the get companiew query to vat companyQuery
            var companyQuery = new GetCompaniesQuery();
            //sends the job query stored in the variable throuth mediater to the query handler
            var jobResult = await _mediator.Send(jobQuery);
            //sends the company query stored in the variable throuth mediater to the query handler
            var companyResult = await _mediator.Send(companyQuery);
            //returns the job result that it recived form the handler
            return View(jobResult);
        }
        //search for a job
        public async Task<IActionResult> Filter(string searchString)
        {
            //stores  the searchString recived from the the user on the query variable
            var query = new FilterQuery { SearchString = searchString };
            //sends the variable to the query handler using mediator and stores the returned result on the var result
            var result = await _mediator.Send(query);
            //reders the view to the user
            return View("Index", result);
        }
        public async Task<IActionResult> FilterCandidate(string searchString,int id)
        {


            //   var candidates = _context.Candidates.Include(u=>u.User).ThenInclude(t=>t.Talent).ThenInclude(s=>s.Skills).ToList();

            var candidates = await _context.Candidates
                .Where(x => x.JobId == id)
                .Include(x => x.User)
                .Include(x => x.User.Talent)
                .ToListAsync();

            var company = _context.Companies.ToList();
            var talents = _context.Talents.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = candidates.Where(n =>n.User.Talent.FirstName.Contains(searchString) || n.User.Talent.Email.Contains(searchString) ).ToList();
               
                return View("SearchResult", filterResult);
            }

            return View("Candidates", candidates);
        }

        [HttpGet]
        //get details of a job
        public async Task<IActionResult> Detail(int Id)
        {   
            //gets the user id of the current user
            string UserId = _userManager.GetUserId(User);
            //stores the job id and user id of who applied to a job
            ViewBag.IsApplied = _context.Candidates.Where(z => z.JobId == Id && z.UserId == UserId).FirstOrDefault();
          //gets the id from the view and sends it along to the query handler
            var query = new GetJobDetailQuery { Id = Id};
            var jobDetail = await _mediator.Send(query);
            //returns the result retrived form the query handler
              return View(jobDetail);          
        }
        [Authorize(Roles = "Company")]
        //the jobs a copmany posted
        public async Task<IActionResult> YourPosts()
        { 
            //gets the current signed in companies id
            var recruiterId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //sends the aquired recruter id to yourpostsquery
            var jobs = await _mediator.Send(new YourPostsQuery { RecruiterId = recruiterId });
            //sends get companies query 
            var companies = await _mediator.Send(new GetCompaniesQuery());  
            //returns the result recived from the handler
            return View(jobs);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> PostJob()
        {
            PostAJobViewModel vm = new PostAJobViewModel();
            //adds skills and education types  to the dropdown
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
            //adds job types to the drop down
            vm.JobTypes = listItems;
            //returns the form to the users to fill
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> PostJob(PostAJobViewModel model)
        {
            if (ModelState.IsValid)
            {      
                //creates a command along with the model and stores it to command variable
                var command = new CreateJobCommand { Model = model };
                //sends the stored command through mediator to handler and stores the result in var result
                var result = await _mediator.Send(command);
             
                return View("RegisterComplete");
            }
            //if the model state is not valid it retrives all the datas for the dropdown again and renders the view
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
            //sends a new get job query with the specified id
            var query = new GetJobQuery { JobId = Id };
            //sends the stored query to the handler and stroes the result in the model
            var model = await _mediator.Send(query);
            //returns the model which is filled with the details of the filled form
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> DeleteJob(PostAJobViewModel model)
        {   
            //sends delete job command along with the model id and stores the result in var result
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
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateJobCommand { Model = model });
                return RedirectToAction("YourPosts");
            }
            //If model state is not valid, return the same view with the same model
            model.Skills = _context.Skills.Select
                        (x => new SelectListItem
                        {
                            Text = x.SkillName,
                            Value = x.Id.ToString()
                        }).ToList();
            model.JobTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "FullTime", Text = "FullTime" },
                    new SelectListItem { Value = "PartTime", Text = "PartTime" }
                };
            model.EducationTypes = await Educations();
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
           // return result;
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
