namespace FamWallet.Services.AccountTransactions.DTOs
{
    public class TransactionDto
    {
        public int ReceiverWalletId { get; set; }
        public int SenderWalletId { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
    }
}
