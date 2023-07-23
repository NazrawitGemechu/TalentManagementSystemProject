using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Identity.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.ViewModels;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;



namespace TalentManagement.Application.Commands.CommandHandler
{
    public class CreateTalentCommandHandler : IRequestHandler<CreateTalentCommand, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
      
        public CreateTalentCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _currentUserService = currentUserService;
 
        }

        public async Task<IActionResult> Handle(CreateTalentCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            model.Skills = await BindSkills();
            model.EducationLevels = await BindEducationLeves();

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
                ApplicantId = await _currentUserService.GetCurrentUserId(),
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
            //save the datas
            _context.Add(talent);
            await _context.SaveChangesAsync();
            return new OkResult();
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
    }
}
