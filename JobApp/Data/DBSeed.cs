using JobApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Data
{
    public static class DBSeed
    {
        public static void Seed(JobAppContext context)
        {
            #region App Users

            if (context.Admin.Any(a => a.Email == "admin@mail.com") == false)
            {
                context.Admin.Add(new Admin
                {
                    // ID = 1
                    Name = "admin",
                    Email = "admin@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                context.SaveChanges();
            }

            if (context.Publisher.Any(p => p.Email == "pub@mail.com") == false)
            {
                context.Publisher.Add(new Publisher
                {
                    // ID = 1
                    Name = "pub",
                    Email = "pub@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                context.SaveChanges();
            }

            if (context.Seeker.Any(s => s.Email == "seek@mail.com") == false)
            {
                context.Seeker.Add(new Seeker
                {
                    // ID = 1
                    Name = "seek",
                    Email = "seek@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                context.SaveChanges();
            }

            if (context.Seeker.Any(s => s.Email == "bum@mail.com") == false)
            {
                context.Seeker.Add(new Seeker
                {
                    // ID = 2
                    Name = "bum",
                    Email = "bum@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                context.SaveChanges();
            }

            if (context.Seeker.Any(s => s.Email == "fired@mail.com") == false)
            {
                context.Seeker.Add(new Seeker
                {
                    // ID = 3
                    Name = "fired",
                    Email = "fired@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                context.SaveChanges();
            }

            #endregion

            #region Regions & SeekerRegion

            if (context.Region.Any(r => r.Name == "North") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "North"
                });
                context.SaveChanges();

                context.SeekerRegion.Add(new SeekerRegion
                {
                    SeekerID = 1,
                    RegionName = "North"
                });
                context.SaveChanges();
            }

            if (context.Region.Any(r => r.Name == "Central") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "Central"
                });
                context.SaveChanges();

                context.SeekerRegion.Add(new SeekerRegion
                {
                    SeekerID = 1,
                    RegionName = "Central"
                });
                context.SaveChanges();

                context.SeekerRegion.Add(new SeekerRegion
                {
                    SeekerID = 2,
                    RegionName = "Central"
                });
                context.SaveChanges();

                context.SeekerRegion.Add(new SeekerRegion
                {
                    SeekerID = 3,
                    RegionName = "Central"
                });
                context.SaveChanges();
            }

            if (context.Region.Any(r => r.Name == "South") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "South"
                });
                context.SaveChanges();

                context.SeekerRegion.Add(new SeekerRegion
                {
                    SeekerID = 1,
                    RegionName = "South"
                });
                context.SaveChanges();
            }

            #endregion

            #region Cities

            if (context.City.Any(c => c.Name == "Haifa") == false)
            {
                context.City.Add(new City
                {
                    Name = "Haifa",
                    RegionName = "North"
                });
                context.SaveChanges();
            }

            if (context.City.Any(c => c.Name == "Herzliya") == false)
            {
                context.City.Add(new City
                {
                    Name = "Herzliya",
                    RegionName = "Central"
                });
                context.SaveChanges();
            }

            if (context.City.Any(c => c.Name == "Tel Aviv") == false)
            {
                context.City.Add(new City
                {
                    Name = "Tel Aviv",
                    RegionName = "Central"
                });
                context.SaveChanges();
            }

            if (context.City.Any(c => c.Name == "Beer Sheva") == false)
            {
                context.City.Add(new City
                {
                    Name = "Beer Sheva",
                    RegionName = "South"
                });
                context.SaveChanges();
            }

            #endregion

            #region Skills & SeekerSkill

            if (context.Skill.Any(s => s.Name == "CPP") == false)
            {
                context.Skill.Add(new Skill
                {
                    Name = "CPP"
                });
                context.SaveChanges();

                context.SeekerSkill.Add(new SeekerSkill
                {
                    SeekerID = 1,
                    SkillName = "CPP"
                });
                context.SaveChanges();
            }

            if (context.Skill.Any(s => s.Name == "C#") == false)
            {
                context.Skill.Add(new Skill
                {
                    Name = "C#"
                });
                context.SaveChanges();

                context.SeekerSkill.Add(new SeekerSkill
                {
                    SeekerID = 1,
                    SkillName = "C#"
                });
                context.SaveChanges();

                context.SeekerSkill.Add(new SeekerSkill
                {
                    SeekerID = 2,
                    SkillName = "C#"
                });
                context.SaveChanges();

                context.SeekerSkill.Add(new SeekerSkill
                {
                    SeekerID = 3,
                    SkillName = "C#"
                });
                context.SaveChanges();
            }

            #endregion

            #region Jobs & related M2M

            if (context.Job.Any(j => j.ID == 1) == false)
            {
                context.Job.Add(new Job
                {
                    PublisherId = 1,
                    Title = "C# Developer",
                    Description = "5+ years of experience",
                    Lon = 34.805718382029376,
                    Lat = 32.157210281107496,
                    LastEdited = new DateTime(2020, 1, 1, 0, 0, 0)
                });
                context.SaveChanges();

                context.JobSkill.Add(new JobSkill
                {
                    JobID = 1,
                    SkillName = "C#"
                });
                context.SaveChanges();

                context.CityJob.Add(new CityJob
                {
                    CityName = "Herzliya",
                    JobID = 1
                });
                context.SaveChanges();

                context.SeekerJob.Add(new SeekerJob
                {
                    SeekerID = 1,
                    JobID = 1,
                    SubmitDate = new DateTime(2020, 1, 2, 12, 0, 0)
                });
                context.SaveChanges();

                context.SeekerJob.Add(new SeekerJob
                {
                    SeekerID = 2,
                    JobID = 1,
                    SubmitDate = new DateTime(2020, 3, 1, 16, 0, 0)
                });
                context.SaveChanges();

                context.SeekerJob.Add(new SeekerJob
                {
                    SeekerID = 3,
                    JobID = 1,
                    SubmitDate = new DateTime(2020, 5, 1, 20, 0, 0)
                });
                context.SaveChanges();
            }

            #endregion
        }
    }
}
