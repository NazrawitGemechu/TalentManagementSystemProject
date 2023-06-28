using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public virtual List<TalentSkill> Talents { get; set; }
        public virtual List<JobSkill> Jobs { get; set; }
    }
}
