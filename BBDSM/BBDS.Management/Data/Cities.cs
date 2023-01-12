using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class City
    {
        public City()
        {
            Users = new HashSet<ApplicationUser>();
        }
        public Guid Id { get; set; }
        public string CityName { get; set; } = null!;
        [InverseProperty(nameof(ApplicationUser.City))]
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
