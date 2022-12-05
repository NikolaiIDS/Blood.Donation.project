using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;

namespace BBDS.Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       /* protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser {
                    Id = "c1980b05-520e-4fca-b556-86ffa4f75e7e",
                    UserName = "Admin Nikolay",
                    NormalizedUserName = "ADMIN NIKOLAY",
                    Email = "nikolaisimeonov60@gmail.com",
                    NormalizedEmail = "NIKOLAISIMEONOV60@GMAIL.COM",
                    PasswordHash = "",
                    PhoneNumber = "0885910355",
                    PhoneNumberConfirmed = true,
                    

                });
            
            base.OnModelCreating(builder);
            
        }*/
    }
}