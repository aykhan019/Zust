using System.ComponentModel.DataAnnotations;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents the model for the registration form.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the username for the registration.
        /// </summary>
        [Required]
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the email address for the registration.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the registration.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the list of errors encountered during the registration process.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
    }
}
