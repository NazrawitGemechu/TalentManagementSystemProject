using System.ComponentModel.DataAnnotations;

namespace TalentManagement.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remeber me?")]
        public bool RememberMe { get; set; }
    }
}
