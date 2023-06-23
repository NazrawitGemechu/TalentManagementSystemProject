﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
    public class JobSkill
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
