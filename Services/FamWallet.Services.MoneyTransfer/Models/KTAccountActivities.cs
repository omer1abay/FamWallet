namespace FamWallet.Services.MoneyTransfer.Models
{
    public class KTAccountActivities
    {
        public int suffix { get; set; }
        public string? date { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public string? transactionReference { get; set; }
        public string? fxCode { get; set; }
        public string? transactionCode { get; set; }

    }
}
