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
            List<TalentSkill> talentSkills = new List<TalentSkill>();
            List<TalentEducationLevel> talentEducationLevels = new List<TalentEducationLevel>();
            var talent = await _context.Talents
                                       .Include(t => t.Skills)
                                       .Include(t => t.EducationLevels)
                                       .Include(t => t.TalentExperiences)
                                       .FirstOrDefaultAsync(t => t.Id == model.Id);
            if (talent != null)
            {
                // Update the talent details
                talent.FirstName = model.FirstName;
                talent.LastName = model.LastName;
                talent.Gender = model.Gender;
                talent.Country = model.Country;
                talent.Email = model.Email;
                talent.Language = model.Language;
                talent.PhoneNo = model.PhoneNo;
                talent.TalentExperiences = model.TalentExperiences;
                talent.ApplicantId = await _currentUserService.GetCurrentUserId();

                // Update the talent skills
                talent.Skills.Clear();
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

                // Update the talent education levels
                talent.EducationLevels.Clear();
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
                if (model.FileCV != null)
                {
                    string uinqueFileName = CVUpload(model);
                    talent.FilePath = uinqueFileName;

                }
                _context.Attach(talent);
                _context.Entry(talent).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return new OkResult();
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
