using System.ComponentModel.DataAnnotations;

namespace FamWallet.IdentityServer.DTOs
{
    public class SignupDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
