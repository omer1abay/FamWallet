using FamWallet.Services.MoneyTransfer.Models;
using FamWallet.Shared.DTOs;

namespace FamWallet.Services.MoneyTransfer.Services
{
    public interface ITransactionService
    {
        public Task<ResponseDto<KTAccountTransactionsResponseModel>> GetTransactions();
        public string GetToken();
    }
}
