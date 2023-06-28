using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class EducationLevel
    {
        public int Id { get; set; }
        public string EducationLevelName { get; set; }
        public virtual List<TalentEducationLevel> Talents { get; set; }
    }
}
