using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TalentManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser
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
        [NotMapped]
        public virtual ICollection<Job> MyJobs { get; set; }
      
       public virtual Talent Talent { get; set; }   

      
    }
}
