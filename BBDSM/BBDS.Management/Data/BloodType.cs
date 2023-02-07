using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class BloodType
    {
        public BloodType()
        {
            Users = new HashSet<ApplicationUser>();
            Requests = new HashSet<Request>();
        }
        public int Id { get; set; }

        [MaxLength (5)]
        public string TypeName { get; set; } = null!;

        [InverseProperty(nameof(ApplicationUser.BloodType))]
        public ICollection<ApplicationUser> Users { get; set; }

        [InverseProperty(nameof(Request.BloodType))]
        public ICollection<Request> Requests { get; set; }
    }
}
