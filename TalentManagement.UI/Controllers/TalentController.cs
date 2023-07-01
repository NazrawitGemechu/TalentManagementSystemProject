using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TalentManagement.UI.Controllers
{
    public class TalentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public TalentController(IMediator mediator, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
            var talents = await _mediator.Send(new GetAllTalentsQuery());
            return View(talents);
        }
        public async Task<IActionResult> Create()
        {         
            var vm = new CreateTalentViewModel()
            {
                Skills = await BindSkills(),
                EducationLevels = await BindEducationLeves()

            };
            vm.TalentExperiences.Add(new TalentExperience() { Id = 1 });
           
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTalentViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (IsDuplicateTalent(model.FirstName, model.LastName, model.Email) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Talent talent = new Talent()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Gender = model.Gender,
                        Country = model.Country,
                        Language = model.Language,
                        PhoneNo = model.PhoneNo,
                        TalentExperiences = model.TalentExperiences,
                        FilePath = CVUpload(model),
                    };

                    foreach (var item in model.SelectedSkills)
                    {
                        talent.Skills.Add(new TalentSkill()
                        {
                            SkillId = item
                        });
                    }

                    foreach (var item in model.SelectedEducation)
                    {
                        talent.EducationLevels.Add(new TalentEducationLevel()
                        {
                            EducationLevelId = item
                        });

                    }
                    var command = new CreateTalentCommand() { NewTalent = talent };
                    var result = await _mediator.Send(command);
                    return RedirectToAction("Index");
                }
            }
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
        public string CVUpload(CreateTalentViewModel model)
        {
          
            string uniqueFileName = null;
            if (model.FileCV != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "file");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileCV.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.FileCV.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private bool IsDuplicateTalent(string fname, string lname, string email)
        {
            //var talents = _mediator.Send(new GetAllTalentsQuery());
            return _context.Talents.Any(d => d.FirstName == fname && d.LastName == lname && d.Email==email);
        }
        
    }
}
