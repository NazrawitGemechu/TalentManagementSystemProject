using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalPosts { get; set; }
        public int TotalPending { get; set; }
        public int TotalRejected { get; set; }
        public int Roles { get; set; }
        public int RejectedTalents { get; set; }
        public int PendingTalents { get; set; }
        public int TotalTalents { get; set; }
        public int TotalUsers { get; set; }
        public int TotalSkills { get; set; }
        public int TotalEducationLevels { get; set; }
        public IEnumerable<Job> RecentPosts { get; set; }
    }
}
