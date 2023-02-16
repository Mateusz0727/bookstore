using System.ComponentModel.DataAnnotations;

namespace Bookshop.App.Models.Auth
{
    public class LoginFormModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
