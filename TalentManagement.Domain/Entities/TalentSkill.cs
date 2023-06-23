using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class TalentSkill
    {
        public int TalentId { get; set; }
        public Talent Talent { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
