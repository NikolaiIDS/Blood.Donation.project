using BBDS.Management.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace bbds.management.data.configurations
{
    public static class ApplicationDbInitializer
    {
        public static void Seed(this ModelBuilder builder)
        {
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                List<ApplicationUser> users = new List<ApplicationUser>()
    {
                 new ApplicationUser {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "MEDIC@GMAIL.COMM",
                },

                new ApplicationUser {
                    UserName = "medic",
                    NormalizedUserName = "MEDIC",
                    Email = "medic@gmail.com",
                    NormalizedEmail = "MEDIC@GMAIL.COMM",
                },
                      new ApplicationUser {
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    Email = "nikolov3007@gmail.com",
                    NormalizedEmail = "NIKOLOV3007@GMAIL.COM",
                },
    };


                builder.Entity<ApplicationUser>().HasData(users);
            }
        }
    }
}

