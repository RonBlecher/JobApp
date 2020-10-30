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

            if (db_changed)
            {
                context.SaveChanges();
            }
        }
    }
}
