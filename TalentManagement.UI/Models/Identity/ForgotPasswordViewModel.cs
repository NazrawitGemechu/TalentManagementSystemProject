using System.ComponentModel.DataAnnotations;

namespace TalentManagement.UI.Models.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
