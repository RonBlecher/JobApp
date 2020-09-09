﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobApp.Models;

namespace JobApp.Data
{
    public class JobAppContext : DbContext
    {
        public JobAppContext (DbContextOptions<JobAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityJob>().HasKey(cj => new { cj.CityName, cj.JobID });
            modelBuilder.Entity<CityJob>()
                .HasOne(cj => cj.Job)
                .WithMany(j => j.JobCities)
                .HasForeignKey(cj => cj.JobID);
            modelBuilder.Entity<CityJob>()
                .HasOne(cj => cj.City)
                .WithMany(c => c.CityJobs)
                .HasForeignKey(cj => cj.CityName);

            modelBuilder.Entity<JobSkill>().HasKey(js => new { js.JobID, js.SkillName });
            modelBuilder.Entity<JobSkill>()
                .HasOne(js => js.Skill)
                .WithMany(s => s.SkillJobs)
                .HasForeignKey(js => js.SkillName);
            modelBuilder.Entity<JobSkill>()
                .HasOne(js => js.Job)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(js => js.JobID);

            modelBuilder.Entity<SeekerJob>().HasKey(sj => new { sj.SeekerID, sj.JobID });
            modelBuilder.Entity<SeekerJob>()
                .HasOne(sj => sj.Job)
                .WithMany(j => j.JobSeekers)
                .HasForeignKey(sj => sj.JobID);
            modelBuilder.Entity<SeekerJob>()
                .HasOne(sj => sj.Seeker)
                .WithMany(s => s.SeekerJobs)
                .HasForeignKey(sj => sj.SeekerID);

            modelBuilder.Entity<SeekerSkill>().HasKey(srsl => new { srsl.SeekerID, srsl.SkillName });
            modelBuilder.Entity<SeekerSkill>()
                .HasOne(srsl => srsl.Skill)
                .WithMany(s => s.SkillSeekers)
                .HasForeignKey(srsl => srsl.SkillName);
            modelBuilder.Entity<SeekerSkill>()
                .HasOne(srsl => srsl.Seeker)
                .WithMany(s => s.SeekerSkills)
                .HasForeignKey(srsl => srsl.SeekerID);
        }

        public DbSet<JobApp.Models.Admin> Admin { get; set; }

        public DbSet<JobApp.Models.City> City { get; set; }

        public DbSet<JobApp.Models.Job> Job { get; set; }

        public DbSet<JobApp.Models.Publisher> Publisher { get; set; }

        public DbSet<JobApp.Models.Region> Region { get; set; }

        public DbSet<JobApp.Models.Seeker> Seeker { get; set; }

        public DbSet<JobApp.Models.Skill> Skill { get; set; }

        public DbSet<JobApp.Models.CityJob> CityJob { get; set; }

        public DbSet<JobApp.Models.JobSkill> JobSkill { get; set; }

        public DbSet<JobApp.Models.SeekerJob> SeekerJob { get; set; }

        public DbSet<JobApp.Models.SeekerSkill> SeekerSkill { get; set; }
    }
}