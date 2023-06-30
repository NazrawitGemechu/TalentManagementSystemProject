using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentManagement.Domain.Entities;
using TalentManagement.Domain.Enum;

namespace TalentManagement.UI.Models.ViewModels
{
    public class CreateTalentViewModel
    {
        [Required(ErrorMessage ="Please Enter Your First Name")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Select Your Gender")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage ="Email Adress is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name ="Phone Number")]
        public int? PhoneNo { get; set; }
        [Required(ErrorMessage ="Please Select Your Country")]
        public Country Country { get; set; }
        [Required(ErrorMessage ="Select your Language Proficency")]
        [Display(Name ="English Language Proficency")]
        public Language Language { get; set; }
        //Skill properties
        [Required(ErrorMessage ="Please Select a Skill")]
        [Display(Name ="Programming Skills")]
        public string SkillName { get; set; }
        //EducationLevel properties
        [Required(ErrorMessage ="Please Select Your Education Level(S)")]
        [Display(Name ="Education Levels")]
        public string EducationLevelName { get; set; }
        //additional information
        public TalentSkill TalentSkill { get; set; }
        [NotMapped]
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        [NotMapped]
        public int[] SelectedSkills { get; set; }= new int[0];
        [NotMapped]
        public TalentEducationLevel TalentEducationLevel { get; set; }
        public List<SelectListItem> EducationLevels { get; set; }= new List<SelectListItem>();
        [NotMapped]
        public int[] SelectedEducation { get; set; }

        //final talent property
        [Required(ErrorMessage = "Please Upload Your CV")]
        [Display(Name = "Upload CV")]
        public IFormFile FileCV { get; set; }
        //Talent Experience properties

        public TalentExperience TalentExperience { get; set; }
        [Required(ErrorMessage ="Experience is Required")]
        public virtual List<TalentExperience> TalentExperiences { get; set; } = new List<TalentExperience>();
        
       


    }
}
