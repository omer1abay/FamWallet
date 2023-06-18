using FamWallet.Services.AccountTransactions.DTOs;
using FamWallet.Shared.DTOs;

namespace FamWallet.Services.AccountTransactions.Services
{
    public interface ITransactionService
    {
        public void AddTransaction(TransactionDto transaction);
        public Task<ResponseDto<List<TransactionDto>>> GetTransaction(string walletId);
    }
}
