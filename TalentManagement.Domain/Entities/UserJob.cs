using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class UserJob
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}
