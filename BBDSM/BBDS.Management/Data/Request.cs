using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class Request
    {
        public Request()
        {
            UsersAcceptedRequest = new HashSet<UsersAcceptedRequests>();
        }
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string BloodTypeName { get; set; }
        
        [MaxLength(50)]
        public string CityName { get; set; }

        public int CountOfRequestedUsers { get; set; }


        [ForeignKey(nameof(BloodType))]
        public int BloodTypeId { get; set; }
        public BloodType BloodType { get; set; } = null!;


        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public City City { get; set; } = null!;

        [InverseProperty(nameof(UsersAcceptedRequests.Request))]
        public ICollection<UsersAcceptedRequests> UsersAcceptedRequest { get; set; }
    }
}
