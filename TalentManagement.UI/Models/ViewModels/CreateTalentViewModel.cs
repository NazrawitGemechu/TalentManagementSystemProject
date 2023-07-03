﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentManagement.Domain.Entities;
using TalentManagement.Domain.Enum;

namespace TalentManagement.UI.Models.ViewModels
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
        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        [Display(Name ="Skill(s)")]
        public int[] SelectedSkills { get; set; }= new int[0];
     
     //   public TalentEducationLevel TalentEducationLevel { get; set; }
        public List<SelectListItem> EducationLevels { get; set; }= new List<SelectListItem>();
        [Display(Name = "Education Level(s)")]
        public int[] SelectedEducation { get; set; }

        //final talent property
        [Required(ErrorMessage = "Please Upload Your CV")]
        [Display(Name = "Upload CV")]
        public IFormFile FileCV { get; set; }
       // public string FilePath { get; set; }
        public virtual List<TalentExperience> TalentExperiences { get; set; } = new List<TalentExperience>();
        
       


    }
}
