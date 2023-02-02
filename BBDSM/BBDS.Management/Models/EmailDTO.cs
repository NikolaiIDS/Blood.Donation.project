using System.ComponentModel.DataAnnotations;

namespace BBDS.Management.Models
{
    public class EmailDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
