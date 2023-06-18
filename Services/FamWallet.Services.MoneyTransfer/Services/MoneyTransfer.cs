using FamWallet.Services.MoneyTransfer.Context;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FamWallet.Services.MoneyTransfer.Services
{
    public class MoneyTransfer : IMoneyTransferService
    {
        public MoneyTransferDbContext _context { get; set; }
        public IHttpClientFactory httpClientFactory { get; set; }
        public MoneyTransfer(MoneyTransferDbContext context, IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _context = context;
        }
        public int DoMoneyTransfer(int receiverWalletNumber, decimal balance, int? senderWalletNumber = null)
        {
            var result = _context.Database.ExecuteSqlInterpolated($"exec sp_UpdBudget {senderWalletNumber},{receiverWalletNumber},{balance}");
            if (result != 0)
            {
                AddTransaction(receiverWalletNumber,senderWalletNumber,balance);
                return result;
            }
            return 0;
        }

        public async void AddTransaction(int receiverWalletId, int? senderWalletId,decimal balance)
        {
            var requestItem = new { Balance = balance,Description = "mock data", ReceiverWalletId = receiverWalletId, senderWalletId = senderWalletId };

            //https://localhost:7077/api/transactions/addtransaction
            var client = httpClientFactory.CreateClient("KuveytTurk");

            var todoItemJson = new StringContent(
                    JsonSerializer.Serialize(requestItem),
                    Encoding.UTF8,
                    Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage =
                await client.PostAsync("https://localhost:7077/api/transactions/addtransaction", todoItemJson);

        }

    }
}
