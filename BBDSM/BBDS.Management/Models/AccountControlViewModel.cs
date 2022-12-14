using System.ComponentModel.DataAnnotations;

namespace BBDS.Management.Models
{
    public class AccountControlViewModel
    {
        public string Id;
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(60)]
        [MinLength(5)]
        public string Email { get; set; } = null!;

        

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$")]
        public string PhoneNumber { get; set; } = null!;

       
    }
}
