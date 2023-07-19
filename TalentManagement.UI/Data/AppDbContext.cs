using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalentManagement.Persistance.Data;
using TalentManagement.Domain.Entities;

namespace TalentManagement.UI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
       // public DbSet<ApplicationUser> ApplicationUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
