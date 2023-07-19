using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TalentManagement.UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="The{0} must be atleast {2} characters long",MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }=new List<SelectListItem>();
        public string RoleSelected { get; set; }
    }
}
