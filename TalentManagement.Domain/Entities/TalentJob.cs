using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class TalentJob
    {
        public int? ApplicantId { get; set; }
        public virtual Talent Applicant { get; set; }

        public int? JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}
