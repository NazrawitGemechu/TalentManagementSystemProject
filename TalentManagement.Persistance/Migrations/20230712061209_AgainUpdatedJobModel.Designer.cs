﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentManagement.Persistance.Data;

#nullable disable

namespace TalentManagement.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230712061209_AgainUpdatedJobModel")]
    partial class AgainUpdatedJobModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TalentManagement.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.EducationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EducationLevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JobDeadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Vacancy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.JobSkill", b =>
                {
                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("JobId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Talent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhoneNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Talents");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentEducationLevel", b =>
                {
                    b.Property<int>("TalentId")
                        .HasColumnType("int");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("int");

                    b.HasKey("TalentId", "EducationLevelId");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("TalentEducationLevel");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfYears")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TalentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TalentId");

                    b.ToTable("TalentExperiences");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentSkill", b =>
                {
                    b.Property<int>("TalentId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("TalentId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("TalentSkill");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Job", b =>
                {
                    b.HasOne("TalentManagement.Domain.Entities.Company", "Company")
                        .WithMany("Jobs")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.JobSkill", b =>
                {
                    b.HasOne("TalentManagement.Domain.Entities.Job", "Job")
                        .WithMany("Skills")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TalentManagement.Domain.Entities.Skill", "Skill")
                        .WithMany("Jobs")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentEducationLevel", b =>
                {
                    b.HasOne("TalentManagement.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany("Talents")
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TalentManagement.Domain.Entities.Talent", "Talent")
                        .WithMany("EducationLevels")
                        .HasForeignKey("TalentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");

                    b.Navigation("Talent");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentExperience", b =>
                {
                    b.HasOne("TalentManagement.Domain.Entities.Talent", null)
                        .WithMany("TalentExperiences")
                        .HasForeignKey("TalentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.TalentSkill", b =>
                {
                    b.HasOne("TalentManagement.Domain.Entities.Skill", "Skill")
                        .WithMany("Talents")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TalentManagement.Domain.Entities.Talent", "Talent")
                        .WithMany("Skills")
                        .HasForeignKey("TalentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Talent");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Company", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.EducationLevel", b =>
                {
                    b.Navigation("Talents");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Job", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Skill", b =>
                {
                    b.Navigation("Jobs");

                    b.Navigation("Talents");
                });

            modelBuilder.Entity("TalentManagement.Domain.Entities.Talent", b =>
                {
                    b.Navigation("EducationLevels");

                    b.Navigation("Skills");

                    b.Navigation("TalentExperiences");
                });
#pragma warning restore 612, 618
        }
    }
}
