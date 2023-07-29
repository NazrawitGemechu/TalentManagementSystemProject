using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentManagement.Domain.Entities;
using TalentManagement.Domain.Enum;


namespace TalentManagement.Application.ViewModels
{
    public class CreateTalentViewModel
    {
        public int Id { get; set; }   
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
    
        [Display(Name ="Phone Number(Optional)")]
        public int? PhoneNo { get; set; }
        [Required(ErrorMessage ="Please Select Your Country")]
        public Country Country { get; set; }
        [Required(ErrorMessage ="Select your Language Proficency")]
        [Display(Name ="English Language Proficency")]
        public Language Language { get; set; }
        [Required(ErrorMessage = "Skill is required. Please select a skill")]
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        [Display(Name ="Select Your Skill(s)")]
        
        public int[] SelectedSkills { get; set; }= new int[0];

        //   public TalentEducationLevel TalentEducationLevel { get; set; }
        [Required(ErrorMessage = "Education Level is required. Please select your Eduacation Level")]
        public List<SelectListItem> EducationLevels { get; set; }= new List<SelectListItem>();
        [Display(Name = "Select Your Education Level(s)")]
       
        public int[] SelectedEducation { get; set; }

        //final talent property
        [Required(ErrorMessage = "Please Upload Your CV")]
        [Display(Name = "Upload CV")]
        [ValidateFile]
        public IFormFile FileCV { get; set; }
        public bool? IsAccepted { get; set; }

        public virtual List<TalentExperience> TalentExperiences { get; set; } = new List<TalentExperience>();
        
       


    }
}
