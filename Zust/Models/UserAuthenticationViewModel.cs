using System.ComponentModel.DataAnnotations;

namespace Zust.Web.Models
{
    public class UserAuthenticationViewModel
    {
        /// <summary>
        /// Gets or sets the username for the login.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the login.
        /// </summary>
        public string? Password { get; set; }
    }
}
