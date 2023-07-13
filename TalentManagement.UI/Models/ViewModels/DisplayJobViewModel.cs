using TalentManagement.Domain.Entities;
using TalentManagement.Domain.Enum;

namespace TalentManagement.UI.Models.ViewModels
{
    public class DisplayJobViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public Country Country { get; set; }
       
        public Company Company { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public int Vacancy { get; set; }
    }
}
