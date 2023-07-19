using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TalentManagement.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public int Vacancy { get; set; }
        public int YearsOfExp { get; set; }
        public string Education { get; set; }
        public int Salary { get; set; }
        //public bool JobStatus { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime JobDeadline { get; set; }

        public virtual List<JobSkill> Skills { get; set; } = new List<JobSkill>();
        public string? JobPosterId { get; set; }
        public virtual ApplicationUser JobPoster { get; set; }
      
    }
}
