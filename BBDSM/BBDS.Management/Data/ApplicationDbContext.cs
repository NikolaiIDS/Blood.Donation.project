using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBDS.Management.Models;

namespace BBDS.Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<LoginViewModel> LoginViewModels { get; set; }
        public DbSet<RegisterViewModel> RegisterViewModels { get; set; }
        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
    }
}