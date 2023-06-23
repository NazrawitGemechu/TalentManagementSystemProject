using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class TalentEducationLevel
    {
        public int TalentId { get; set; }
        public Talent Talent { get; set; }
        public int EducationLevelId { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}
