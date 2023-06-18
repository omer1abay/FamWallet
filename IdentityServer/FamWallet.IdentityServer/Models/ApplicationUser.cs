using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamWallet.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int WalletNumber{ get; set; }
        public decimal Budget { get; set; }
        [ForeignKey("fk_groupid")]
        public int GroupId { get; set; }
    }
}
