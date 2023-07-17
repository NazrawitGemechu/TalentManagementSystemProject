using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //Creating the Main Tables

        public virtual DbSet<Talent> Talents { get; set; }
        public virtual DbSet<TalentExperience> TalentExperiences { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<TalentSkill> TalentSkill { get; set; }
        public virtual DbSet<JobSkill> JobSkill { get; set; }
        public virtual DbSet<TalentEducationLevel> TalentEducationLevel { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }

        //Configuring the Many to Many relationships

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TalentSkill

            modelBuilder.Entity<TalentSkill>().HasKey(ts => new { ts.TalentId, ts.SkillId });
            modelBuilder.Entity<TalentSkill>().
                HasOne(ts => ts.Talent).WithMany(ts => ts.Skills).HasForeignKey(t => t.TalentId);
            modelBuilder.Entity<TalentSkill>().
                HasOne(ts => ts.Skill).WithMany(ts => ts.Talents).HasForeignKey(t => t.SkillId);

            //JobSkill

            modelBuilder.Entity<JobSkill>().HasKey(ts => new { ts.JobId, ts.SkillId });
            modelBuilder.Entity<JobSkill>().
                HasOne(ts => ts.Job).WithMany(ts => ts.Skills).HasForeignKey(t => t.JobId);
            modelBuilder.Entity<JobSkill>().
                HasOne(ts => ts.Skill).WithMany(ts => ts.Jobs).HasForeignKey(t => t.SkillId);

            //TalentEducationLevel

            modelBuilder.Entity<TalentEducationLevel>().HasKey(ts => new { ts.TalentId, ts.EducationLevelId });
            modelBuilder.Entity<TalentEducationLevel>().
                HasOne(ts => ts.Talent).WithMany(ts => ts.EducationLevels).HasForeignKey(t => t.TalentId);
            modelBuilder.Entity<TalentEducationLevel>().
                HasOne(ts => ts.EducationLevel).WithMany(ts => ts.Talents).HasForeignKey(t => t.EducationLevelId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
