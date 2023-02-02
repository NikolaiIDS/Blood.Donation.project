using System.ComponentModel.DataAnnotations;

namespace BBDS.Management.Models
{
    public class RegisterViewModel
    {
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
        [MaxLength(20)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$")]
        public string PhoneNumber { get; set; } = null!;

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
        public int BloodId { get; set; }

        public int GenderId { get; set; }

        public Guid CityId { get; set; }
    }
}
