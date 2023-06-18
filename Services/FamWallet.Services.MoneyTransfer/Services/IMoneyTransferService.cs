namespace FamWallet.Services.MoneyTransfer.Services
{
    public interface IMoneyTransferService
    {
        public int DoMoneyTransfer(int walletNumber,decimal balance, int? senderWalletNumber = null);
    }
}
