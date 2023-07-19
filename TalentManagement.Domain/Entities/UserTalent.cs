using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class UserTalent
    {
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? TalentId { get; set; }
        public virtual Talent Talent { get; set; }
    }
}
