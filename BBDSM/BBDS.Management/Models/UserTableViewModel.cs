using System.ComponentModel.DataAnnotations;

namespace BBDS.Management.Models
{
    public class UserTableViewModel
    {

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int BloodId { get; set; }

        public Guid CityId { get; set; }

        public int SearchCount { get; set; }

        public int SearchedBloodId { get; set; }

        public Guid SearchedCityId { get; set; }

    }
}
