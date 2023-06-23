using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Domain.Entities
{
   public class TalentExperience
    {
        public int Id { get; set; }
        public int TalentId { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public int WorkedForYear { get; set; }
    }
}
