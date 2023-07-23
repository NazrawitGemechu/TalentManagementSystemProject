using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.Application.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography;

namespace TalentManagement.UI.Controllers
{
    [Authorize]
    public class TalentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public TalentController(IMediator mediator, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
        [Authorize(Roles = "Talent,Admin")]
        public async Task<IActionResult> Index()
        {
            var talents = await _mediator.Send(new GetAllTalentsQuery());
            return View(talents);
        }
        //search
        public async Task<IActionResult> Filter(string searchString)
        {
            var talents = await _mediator.Send(new GetAllTalentsQuery());
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = talents.Where(n => n.FirstName.Contains(searchString) || n.Email.Contains(searchString)).ToList();
                return View("Index", filterResult);
            }
            return View("Index", talents);
        }
        //Details
        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var query = new GetTalentDetailQuery { Id = Id };
            var talentDetail = await _mediator.Send(query);
            return View(talentDetail);
        }
        [Authorize(Roles = "Talent")]
        public async Task<IActionResult> Resume()
        {
            var query = new GetTalentsByApplicantIdQuery { ApplicantId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value };
            var model = await _mediator.Send(query);

            return View(model);
        }

        [Authorize(Roles = "Talent")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateTalentViewModel()
            {
                Skills = await BindSkills(),
                EducationLevels = await BindEducationLeves()
            };
            vm.TalentExperiences.Add(new TalentExperience() { Id = 1 });
            string UserId = _userManager.GetUserId(User);
            var talents = await _mediator.Send(new GetAllTalentsQuery());
            var talent = talents.Where(x => x.ApplicantId == UserId).FirstOrDefault();
            if (talent != null)
            {
                return View("AlreadyUploaded");
            }
            return View(vm);
        }
        [Authorize(Roles = "Talent")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTalentViewModel model)
        {
            model.Skills = await BindSkills();
            model.EducationLevels = await BindEducationLeves();

            if (ModelState.IsValid)
            {
                var command = new CreateTalentCommand { Model = model };
                var result = await _mediator.Send(command);
                // return result;
                return View("RegisterComplete");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Talent")]
        public async Task<IActionResult> Edit(int Id)
        {
            var query = new GetTalentQuery { TalentId = Id };
            var model = await _mediator.Send(query);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Talent")]
        public async Task<IActionResult> Edit(CreateTalentViewModel model)
        {
            Talent talent = new Talent();
            List<TalentSkill> talentSkills = new List<TalentSkill>();
            List<TalentEducationLevel> talentEducationLevels = new List<TalentEducationLevel>();
            model.Skills = await BindSkills();
            model.EducationLevels = await BindEducationLeves();

            if (ModelState.IsValid)
            {
                var command = new UpdateTalentCommand { Model = model };
                var result = await _mediator.Send(command);

                return RedirectToAction("Resume");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Talent")]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new GetTalentQuery { TalentId = Id };
            var model = await _mediator.Send(query);

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Talent")]
        public async Task<IActionResult> Delete(CreateTalentViewModel model)
        {
            var command = new DeleteTalentCommand { TalentId = model.Id };
            var result = await _mediator.Send(command);
            if (result)
                return RedirectToAction("Resume");

            return RedirectToAction("Delete");

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
        public async Task<List<SelectListItem>> BindEducationLeves()
        {
            var educationFromDb = _mediator.Send(new GetAllEducationLevelsQuery());
            var selectListE = new List<SelectListItem>();
            foreach (var item in await educationFromDb)
            {
                selectListE.Add(new SelectListItem(item.EducationLevelName, item.Id.ToString()));
            }
            return selectListE;
        }
        public  IActionResult DownloadCV(string fileName)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "file", fileName);
            byte[] fileBytes =  System.IO.File.ReadAllBytes(filePath);
            return  File(fileBytes, "application/octet-stream", fileName);
            
        }
    }
}
