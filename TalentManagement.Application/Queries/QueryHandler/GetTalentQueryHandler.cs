using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Application.ViewModels;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetTalentQueryHandler : IRequestHandler<GetTalentQuery, CreateTalentViewModel>
    {
        private readonly ApplicationDbContext _context;

        public GetTalentQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateTalentViewModel> Handle(GetTalentQuery request, CancellationToken cancellationToken)
        {
            CreateTalentViewModel model = new CreateTalentViewModel();

            List<int> skillsIds = new List<int>();
            List<int> educationIds = new List<int>();

            // Get talent
            var talent = await _context.Talents.Include(t => t.Skills).FirstOrDefaultAsync(x => x.Id == request.TalentId);

            // Get talent skills and add each skillId into selectedskills list
            talent.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));

            // Get talent with education levels
            var talentE = await _context.Talents.Include(t => t.EducationLevels).FirstOrDefaultAsync(x => x.Id == request.TalentId);
            talent.EducationLevels.ToList().ForEach(result => educationIds.Add(result.EducationLevelId));

            Talent tal = await _context.Talents.Include(e => e.TalentExperiences)
                                               .Where(a => a.Id == request.TalentId)
                                               .FirstOrDefaultAsync();

            // Bind model
            model.Skills = await _context.Skills.Select(x => new SelectListItem
            {
                Text = x.SkillName,
                Value = x.Id.ToString()
            }).ToListAsync();

            model.EducationLevels = await _context.EducationLevels.Select(x => new SelectListItem
            {
                Text = x.EducationLevelName,
                Value = x.Id.ToString()
            }).ToListAsync();

            model.TalentExperiences = tal.TalentExperiences;
            model.Id = talent.Id;
            model.FirstName = talent.FirstName;
            model.LastName = talent.LastName;
            model.Country = talent.Country;
            model.Gender = talent.Gender;
            model.Email = talent.Email;
            model.PhoneNo = talent.PhoneNo;
            model.FileCV = talent.FileCV;
            model.Language = talent.Language;
            model.SelectedSkills = skillsIds.ToArray();
            model.SelectedEducation = educationIds.ToArray();

            return model;
        }
    }
}
