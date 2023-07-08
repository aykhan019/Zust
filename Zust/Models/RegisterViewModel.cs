using System.ComponentModel.DataAnnotations;

namespace Zust.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
