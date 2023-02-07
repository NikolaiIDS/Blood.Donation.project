using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UsersAcceptedRequest = new HashSet<UsersAcceptedRequests>();
        }
        [Required]
        [MaxLength(20, ErrorMessage = "Собственото име трябва да е по-малко от 20 символа!")]
        [MinLength(3, ErrorMessage = "Собственото име трябва да е повече от 2 символа!")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "Фамилното име трябва да е по-малко от 20 символа!")]
        [MinLength(3, ErrorMessage = "Фамилното име трябва да е повече от 2 символа!")]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(@"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage = "ЕГН-то трябва да е 10-цифрено число!")]
        public string EGN { get; set; } = null!;

        public int GenderId { get; set; }

        [ForeignKey(nameof(BloodType))]
        public int BloodTypeId { get; set; }
        public BloodType BloodType { get; set; } = null!;


        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public City City { get; set; } = null!;

        [InverseProperty(nameof(UsersAcceptedRequests.User))]
        public ICollection<UsersAcceptedRequests> UsersAcceptedRequest { get; set; }
    }
}
