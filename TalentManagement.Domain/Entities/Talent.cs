using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Enum;

namespace TalentManagement.Domain.Entities
{
    public class Talent
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int? PhoneNo {get; set; }
        public Gender Gender { get; set; }
        public Country Country { get; set; }
        [NotMapped]      
        public IFormFile FileCV { get; set; }
        public string FilePath { get; set; } = "";
        public Language Language { get; set; }
        public bool? IsAccepted { get; set; }
        public virtual List<TalentExperience> TalentExperiences { get; set; } = new List<TalentExperience>();
        public virtual List<TalentSkill> Skills { get; set; } = new List<TalentSkill>();
        public virtual List<TalentEducationLevel> EducationLevels { get; set; } = new List<TalentEducationLevel>();

        public string? ApplicantId { get; set; }
        public virtual ApplicationUser Applicant { get; set; }
       

       

    }
}
