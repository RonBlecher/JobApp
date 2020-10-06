﻿// <auto-generated />
using System;
using JobApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobApp.Migrations
{
    [DbContext(typeof(JobAppContext))]
    [Migration("20201006190654_Annotations")]
    partial class Annotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JobApp.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("JobApp.Models.City", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("RegionName");

                    b.ToTable("City");
                });

            modelBuilder.Entity("JobApp.Models.CityJob", b =>
                {
                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.HasKey("CityName", "JobID");

                    b.HasIndex("JobID");

                    b.ToTable("CityJob");
                });

            modelBuilder.Entity("JobApp.Models.Job", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublisherID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("JobApp.Models.JobSkill", b =>
                {
                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("JobID", "SkillName");

                    b.HasIndex("SkillName");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("JobApp.Models.Publisher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("JobApp.Models.Region", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("JobApp.Models.Seeker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("CV")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Seeker");
                });

            modelBuilder.Entity("JobApp.Models.SeekerJob", b =>
                {
                    b.Property<int>("SeekerID")
                        .HasColumnType("int");

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.HasKey("SeekerID", "JobID");

                    b.HasIndex("JobID");

                    b.ToTable("SeekerJob");
                });

            modelBuilder.Entity("JobApp.Models.SeekerSkill", b =>
                {
                    b.Property<int>("SeekerID")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SeekerID", "SkillName");

                    b.HasIndex("SkillName");

                    b.ToTable("SeekerSkill");
                });

            modelBuilder.Entity("JobApp.Models.Skill", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("JobApp.Models.City", b =>
                {
                    b.HasOne("JobApp.Models.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApp.Models.CityJob", b =>
                {
                    b.HasOne("JobApp.Models.City", "City")
                        .WithMany("CityJobs")
                        .HasForeignKey("CityName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobApp.Models.Job", "Job")
                        .WithMany("JobCities")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApp.Models.Job", b =>
                {
                    b.HasOne("JobApp.Models.Publisher", "Publisher")
                        .WithMany("PostedJobs")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApp.Models.JobSkill", b =>
                {
                    b.HasOne("JobApp.Models.Job", "Job")
                        .WithMany("JobSkills")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobApp.Models.Skill", "Skill")
                        .WithMany("SkillJobs")
                        .HasForeignKey("SkillName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApp.Models.SeekerJob", b =>
                {
                    b.HasOne("JobApp.Models.Job", "Job")
                        .WithMany("JobSeekers")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobApp.Models.Seeker", "Seeker")
                        .WithMany("SeekerJobs")
                        .HasForeignKey("SeekerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobApp.Models.SeekerSkill", b =>
                {
                    b.HasOne("JobApp.Models.Seeker", "Seeker")
                        .WithMany("SeekerSkills")
                        .HasForeignKey("SeekerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobApp.Models.Skill", "Skill")
                        .WithMany("SkillSeekers")
                        .HasForeignKey("SkillName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
