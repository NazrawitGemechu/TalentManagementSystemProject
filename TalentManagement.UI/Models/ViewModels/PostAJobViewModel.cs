using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TalentManagement.Domain.Entities;
using TalentManagement.Domain.Enum;

namespace TalentManagement.UI.Models.ViewModels
{
    public class PostAJobViewModel
    {
        public int Id { get; set; } 
        [Required(ErrorMessage ="Company Name is Required")]
        [Display(Name ="Comapny Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Company Email is Required")]
        [Display(Name = "Comapny Email")]
        public string CompanyEmail { get; set; }
        [Required(ErrorMessage = "Country is Required")]
      
        public Country Country { get; set; }
        [Required(ErrorMessage = "Please Enter the Job Title")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Job description is required")]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Required(ErrorMessage = "Please select the Job Type")]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }
        
        public IEnumerable<SelectListItem> JobTypes { get; set; } = new List<SelectListItem>();
        [Required(ErrorMessage = "Please fill the No of talents you require")]
        [Display(Name = "Vacancy")]
        public int Vacancy { get; set; }
        [Required(ErrorMessage = "Please fill years of experience")]
        [Display(Name = "Years of Experience")]
        public int YearsOfExp { get; set; }
        [Required(ErrorMessage = "Please fill amount of salary")]
        [Display(Name = "Salary")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Please fill required higher education level")]
        [Display(Name = "Primary Degree or Masters in :")]
        public string Education { get; set; }
        public IEnumerable<SelectListItem> EducationTypes { get; set; } = new List<SelectListItem>();
        // public string JobStatus { get; set; }
        [Required(ErrorMessage = "Posted date is Required")]
        [Display(Name = "Job Application Start Date")]
        public DateTime PostedDate { get; set; }
        [Required(ErrorMessage = "Deadline is Required")]
        [Display(Name = "Job Application Deadline")]
        public DateTime JobDeadline { get; set; }
        public bool? IsAccepted { get; set; }
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        [Display(Name ="Required Skills:")]
        public int[] SelectedSkills { get; set; }
        public virtual List<Job> Jobs { get; set; } = new List<Job>();
    }
}
