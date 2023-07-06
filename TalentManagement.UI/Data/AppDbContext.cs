using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Models.Identity;

namespace TalentManagement.UI.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions  options) : base(options) { }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    
    }
}
