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
            bool db_changed = false;

            if (context.Admin.Any(a => a.Email == "admin@mail.com") == false)
            {
                context.Admin.Add(new Admin
                {
                    Name = "admin",
                    Email = "admin@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                db_changed = true;
            }

            if (context.Seeker.Any(s => s.Email == "seek@mail.com") == false)
            {
                context.Seeker.Add(new Seeker
                {
                    Name = "seek",
                    Email = "seek@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                db_changed = true;
            }

            if (context.Publisher.Any(p => p.Email == "pub@mail.com") == false)
            {
                context.Publisher.Add(new Publisher
                {
                    Name = "pub",
                    Email = "pub@mail.com",
                    PhoneNum = "050-0055555",
                    Password = "123456"
                });
                db_changed = true;
            }

            if (context.Region.Any(r => r.Name == "North") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "North"
                });
                db_changed = true;
            }

            if (context.Region.Any(r => r.Name == "Central") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "Central"
                });
                db_changed = true;
            }

            if (context.Region.Any(r => r.Name == "South") == false)
            {
                context.Region.Add(new Region
                {
                    Name = "South"
                });
                db_changed = true;
            }

            if (context.City.Any(c => c.Name == "Haifa") == false)
            {
                context.City.Add(new City
                {
                    Name = "Haifa",
                    RegionName = "North"
                });
                db_changed = true;
            }

            if (context.City.Any(c => c.Name == "Herzliya") == false)
            {
                context.City.Add(new City
                {
                    Name = "Herzliya",
                    RegionName = "Central"
                });
                db_changed = true;
            }

            if (context.City.Any(c => c.Name == "Tel Aviv") == false)
            {
                context.City.Add(new City
                {
                    Name = "Tel Aviv",
                    RegionName = "Central"
                });
                db_changed = true;
            }

            if (context.City.Any(c => c.Name == "Beer Sheva") == false)
            {
                context.City.Add(new City
                {
                    Name = "Beer Sheva",
                    RegionName = "South"
                });
                db_changed = true;
            }

            if (context.Skill.Any(s => s.Name == "CPP") == false)
            {
                context.Skill.Add(new Skill
                {
                    Name = "CPP"
                });
                db_changed = true;
            }

            if (context.Skill.Any(s => s.Name == "C#") == false)
            {
                context.Skill.Add(new Skill
                {
                    Name = "C#"
                });
                db_changed = true;
            }

            if (db_changed)
            {
                context.SaveChanges();
            }
        }
    }
}
