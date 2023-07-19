using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TalentManagement.Domain.Entities;

namespace TalentManagement.UI.Models.Identity
{
    public class ApplicationUser 
    {
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string? Role { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();

        public virtual ICollection<Job> MyJobs { get; set; }

    }
}
