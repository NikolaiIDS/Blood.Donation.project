using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
