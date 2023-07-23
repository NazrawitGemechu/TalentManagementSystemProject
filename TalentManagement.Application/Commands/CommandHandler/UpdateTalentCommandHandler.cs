using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UpdateTalentCommandHandler : IRequestHandler<UpdateTalentCommand, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTalentCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _currentUserService = currentUserService;

        }
        public async Task<IActionResult> Handle(UpdateTalentCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            Talent talent = new Talent();
            List<TalentSkill> talentSkills = new List<TalentSkill>();
            List<TalentEducationLevel> talentEducationLevels = new List<TalentEducationLevel>();
            model.Skills = await BindSkills();
            model.EducationLevels = await BindEducationLeves();

            //first find talent skill list and then remove all from db 
            talent = _context.Talents.Include("Skills").FirstOrDefault(x => x.Id == model.Id);
            talent.Skills.ToList().ForEach(result => talentSkills.Add(result));
            _context.TalentSkill.RemoveRange(talentSkills);
            _context.SaveChanges();
            //second find talent education level list and remove all from db
            talent = _context.Talents.Include("EducationLevels").FirstOrDefault(x => x.Id == model.Id);
            talent.EducationLevels.ToList().ForEach(result => talentEducationLevels.Add(result));
            _context.TalentEducationLevel.RemoveRange(talentEducationLevels);
            _context.SaveChanges();
            //third find talent experience list and remove all from db
            List<TalentExperience> talentExp = _context.TalentExperiences.Where(d => d.TalentId == model.Id).ToList();
            _context.TalentExperiences.RemoveRange(talentExp);
            _context.SaveChanges();
            //fourth update talent details
            talent.FirstName = model.FirstName;
            talent.LastName = model.LastName;
            talent.Gender = model.Gender;
            talent.Country = model.Country;
            talent.Email = model.Email;
            talent.Language = model.Language;
            talent.PhoneNo = model.PhoneNo;
            talent.TalentExperiences = model.TalentExperiences;
            talent.ApplicantId = await _currentUserService.GetCurrentUserId();



            if (model.FileCV != null)
            {
                string uinqueFileName = CVUpload(model);
                talent.FilePath = uinqueFileName;

            }
            //talent skills
            if (model.SelectedSkills.Length > 0)
            {
                talentSkills = new List<TalentSkill>();

                foreach (var skillid in model.SelectedSkills)
                {
                    talentSkills.Add(new TalentSkill { SkillId = skillid, TalentId = model.Id });
                }
                talent.Skills = talentSkills;
            }
            _context.SaveChanges();
            // talent education levels
            if (model.SelectedEducation.Length > 0)
            {
                talentEducationLevels = new List<TalentEducationLevel>();

                foreach (var educationid in model.SelectedEducation)
                {
                    talentEducationLevels.Add(new TalentEducationLevel { EducationLevelId = educationid, TalentId = model.Id });
                }
                talent.EducationLevels = talentEducationLevels;
            }
            _context.SaveChanges();
            // _context.TalentExperiences.AddRange(talent.TalentExperiences);
            _context.Attach(talent);
            _context.Entry(talent).State = EntityState.Modified;
            // _context.TalentExperiences.AddRange(talent.TalentExperiences);
            _context.SaveChanges();
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
