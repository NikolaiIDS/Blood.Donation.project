using System.ComponentModel.DataAnnotations;

namespace BBDS.Management.Models
{
    public class UserEmailViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Guid CityId { get; set; }
        public int BloodId { get; set; }
    }
}
