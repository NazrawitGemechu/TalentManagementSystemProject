using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
   public class TalentExperience
    {
        public int Id { get; set; }
        public int TalentId { get; set; }
        [Required(ErrorMessage = "Please Fill the Company Name")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = "";
        [Required(ErrorMessage = "Please Fill the Company Email Address")]
        [Display(Name = "Company Email Address")]
        [EmailAddress]
        public string CompanyEmailAddress { get; set; } = "";
        [Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; } = "";
        [Required(ErrorMessage = "Please Fill years of Employement")]
        [Display(Name = "Years of Employement")]
        [Range(1,int.MaxValue, ErrorMessage = "Please Fill years of Employement")]
        public int NumberOfYears { get; set; }
    }
}
