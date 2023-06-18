using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamWallet.IdentityServer.Models
{
    public class GroupModel
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int GroupId { get; set; }
        [Required, Column(TypeName = "varchar(50)")] //[Column(TypeName = "varchar(200)")]
        public string GroupName { get; set; }
    }
}
