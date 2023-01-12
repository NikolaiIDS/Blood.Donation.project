using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class BloodType
    {
        public BloodType()
        {
            Users = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }

        [MaxLength (5)]
        public string TypeName { get; set; } = null!;

        [InverseProperty(nameof(ApplicationUser.BloodType))]
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
