using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Zust.Web.Helpers.Constants;

namespace Zust.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
