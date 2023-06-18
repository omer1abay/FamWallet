namespace FamWallet.Services.MoneyTransfer.Models
{
    public class MoneyTransferModel
    {
        public int? SenderWalletNumber { get; set; }
        public int ReceiverWalletNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
