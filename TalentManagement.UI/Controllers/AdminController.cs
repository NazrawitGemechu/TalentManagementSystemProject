using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentManagement.Application.Queries.CompanyQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Models.ViewModels;

namespace TalentManagement.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            AdminDashboardViewModel dashboard = new AdminDashboardViewModel
            {
                TotalPosts = _context.Jobs.Where(x => x.IsAccepted == true).Count(),
                TotalPending = _context.Jobs.Where(x => x.IsAccepted == null).Count(),
                TotalRejected = _context.Jobs.Where(x => x.IsAccepted == false).Count(),
                TotalUsers = _context.ApplicationUser.Count(),
                Roles=_context.Roles.Count(),
                RejectedTalents=_context.Talents.Where(x => x.IsAccepted == false).Count(),
                PendingTalents = _context.Talents.Where(x => x.IsAccepted == null).Count(),
                TotalTalents = _context.Talents.Where(x => x.IsAccepted == true).Count(),
                RecentPosts = _context.Jobs.Where(x => x.IsAccepted == true).OrderByDescending(o => o.PostedDate)
                  .Include(x => x.Skills)
                .Include(x => x.Recruter)

            };
            return View(dashboard);
        }

        // GET: Admin/PendingPosts
        public async Task<IActionResult> PendingPosts()
        {
            //assigns the get companiew query to vat companyQuery
            var companyQuery = new GetCompaniesQuery();
           
            //sends the company query stored in the variable throuth mediater to the query handler
            var companyResult = await _mediator.Send(companyQuery);
            var jobs = _context.Jobs.Where(x => x.IsAccepted == null)
                .Include(x => x.Recruter)
                .ToList();

            return View(jobs);
        }
        // GET: Admin/PendingTalents
        public async Task<IActionResult> PendingTalents()
        {
            var talents = _context.Talents.Where(x => x.IsAccepted == null)
                .Include(x => x.Applicant)
                .ToList();

            return View(talents);
        }
        public async Task<IActionResult> RejectedPosts()
        {
            //assigns the get companiew query to vat companyQuery
            var companyQuery = new GetCompaniesQuery();

            //sends the company query stored in the variable throuth mediater to the query handler
            var companyResult = await _mediator.Send(companyQuery);
            var jobs = _context.Jobs.Where(x => x.IsAccepted == false)
                .Include(x => x.Recruter)
                .ToList();

            return View(jobs);
        }
        public async Task<IActionResult> RejectedTalents()
        {
           
            var talents = _context.Talents.Where(x => x.IsAccepted == false)
                .Include(x => x.Applicant)
                .ToList();

            return View(talents);
        }
        public IActionResult AcceptJob(int jobId)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == jobId);
            if (job != null)
            {
                // Update IsAccepted value to true
                job.IsAccepted = true;

                _context.SaveChanges(); // Save changes to persist the updated value

                return RedirectToAction("Index", "Admin"); // Redirect to admin panel or any other desired page
            }

            return NotFound(); // Job not found
        }
        public IActionResult AcceptTalent(int talentId)
        {
            var talent = _context.Talents.FirstOrDefault(j => j.Id ==talentId);
            if (talent != null)
            {
                // Update IsAccepted value to true
                talent.IsAccepted = true;

                _context.SaveChanges(); // Save changes to persist the updated value

                return RedirectToAction("Index", "Admin"); // Redirect to admin panel or any other desired page
            }

            return NotFound(); // Job not found
        }
        public IActionResult RejectJob(int jobId)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == jobId);
            if (job != null)
            {
                // Update IsAccepted value to true
                job.IsAccepted = false;

                _context.SaveChanges(); // Save changes to persist the updated value

                return RedirectToAction("Index", "Admin"); // Redirect to admin panel or any other desired page
            }

            return NotFound(); // Job not found
        }
        public IActionResult RejectTalent(int talentId)
        {
            var talent = _context.Talents.FirstOrDefault(j => j.Id == talentId);
            if (talent != null)
            {
                // Update IsAccepted value to true
                talent.IsAccepted = false;

                _context.SaveChanges(); // Save changes to persist the updated value

                return RedirectToAction("Index", "Admin"); // Redirect to admin panel or any other desired page
            }

            return NotFound(); // Job not found
        }
    }
}
