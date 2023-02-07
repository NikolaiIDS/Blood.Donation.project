using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;
using BBDS.Management.Data.Configurations;
using System.Diagnostics.Contracts;

namespace BBDS.Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<UsersAcceptedRequests> UsersAcceptedRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserAcceptedRequestsConfigration());
            builder.ApplyConfiguration(new BloodTypeConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            base.OnModelCreating(builder);
        }
    }
}