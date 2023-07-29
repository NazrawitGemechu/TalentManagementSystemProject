using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentManagement.Application.Commands.AdminCommand;
using TalentManagement.Application.Queries.AdminQuery;
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
        public async Task<IActionResult> Index()
        {
            
            var dashboard = await _mediator.Send(new AdminDashboardQuery());
            return View(dashboard);
        }

        // GET: Admin/PendingPosts
        public async Task<IActionResult> PendingPosts()
        {
            
            var postsQuery = new PendingPostsQuery();
            var jobs = await _mediator.Send(postsQuery);
            return View(jobs);
        }
        // GET: Admin/PendingTalents
        public async Task<IActionResult> PendingTalents()
        {
           
            var talentsQuery = new PendingTalentsQuery();
            var talents = await _mediator.Send(talentsQuery);

            return View(talents);
        }
        public async Task<IActionResult> RejectedPosts()
        {
            //assigns the get companiew query to vat companyQuery
            var companyQuery = new GetCompaniesQuery();

            //sends the company query stored in the variable throuth mediater to the query handler
            var companyResult = await _mediator.Send(companyQuery);
           
            var rejectedPostsQuery = new RejectedPostsQuery();
            var jobs = await _mediator.Send(rejectedPostsQuery);

            return View(jobs);
        }
        public async Task<IActionResult> RejectedTalents()
        {

           
            var rejectedTalentsQuery = new RejectedTalentsQuery();
            var talents = await _mediator.Send(rejectedTalentsQuery);
            return View(talents);
        }
        public async Task<IActionResult> AcceptJob(int jobId)
        {
           
            var acceptJobCommand = new AcceptJobCommand { JobId = jobId };

            
                await _mediator.Send(acceptJobCommand);
                return RedirectToAction("Index", "Admin"); 
            
           
        }
        public async Task<IActionResult> AcceptTalent(int talentId)
        {
            

            var acceptTalentCommand = new AcceptTalentCommand { TalentId = talentId };
            await _mediator.Send(acceptTalentCommand);
            return RedirectToAction("Index", "Admin");
        }
               
        
        public async Task<IActionResult> RejectJob(int jobId)
        {
           
            var rejectJobCommand = new RejectJobCommand { JobId = jobId };

            
                await _mediator.Send(rejectJobCommand);
                return RedirectToAction("Index", "Admin"); 
          
        }
        public async Task<IActionResult> RejectTalent(int talentId)
        {
            await _mediator.Send(new RejectTalentCommand { TalentId = talentId });

            return RedirectToAction("Index", "Admin");

        }
    }
}
