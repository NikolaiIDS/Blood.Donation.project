using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;
using BBDS.Management.Data.Configurations;

namespace BBDS.Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BloodType> BloodTypes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BloodTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}