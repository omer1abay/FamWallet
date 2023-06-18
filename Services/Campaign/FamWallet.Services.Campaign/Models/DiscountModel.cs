using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamWallet.Services.Campaign.Models
{
    public class DiscountModel
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
    }
}
