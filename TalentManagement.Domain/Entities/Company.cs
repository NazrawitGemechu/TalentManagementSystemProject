using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Enum;

namespace TalentManagement.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public Country Country { get; set; }
        public virtual List<Job> Jobs { get; set; } = new List<Job>();

       
    }
}
