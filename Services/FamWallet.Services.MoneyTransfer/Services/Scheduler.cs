using FamWallet.Shared.Models;
using Quartz;
using System.Text.Json;

namespace FamWallet.Services.MoneyTransfer.Services
{
    public class Scheduler : IJob
    {

        private readonly ITransactionService _transactionService;
        private readonly IMoneyTransferService _moneyTransferService;
        private readonly string _getUserUrl = "https://localhost:5051/api/user/getuserall";
        private List<UserModel> userModel;
        public IHttpClientFactory httpClientFactory { get; set; }

        public Scheduler(ITransactionService service,IMoneyTransferService moneyTransferService,IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _transactionService = service;
            _moneyTransferService = moneyTransferService;
            GetAllUser();
        }

        public Task Execute(IJobExecutionContext context)
        {
            var response = _transactionService.GetTransactions();

            if (response.Result.IsSuccess)
            {
                foreach (var transaction in response.Result.Data.value.accountActivities)
                {
                    if (transaction.description != null)
                    {
                        var value = SplitAndGetWalletNumber(transaction.description);

                        if (value.Item1)
                        {
                            _moneyTransferService.DoMoneyTransfer(value.Item2, transaction.balance);
                        }

                    }
                }

            }

            return Task.CompletedTask;
        }
        
        private async void GetAllUser()
        {
            HttpClient client = httpClientFactory.CreateClient("KuveytTurk");

            var httpResponse = await client.GetAsync(_getUserUrl);

            if (httpResponse.IsSuccessStatusCode)
            {
                var stream = httpResponse.Content.ReadAsStreamAsync();
                userModel = JsonSerializer.Deserialize<List<UserModel>>(stream.Result);
            }

        }

        private (bool,int) SplitAndGetWalletNumber(string description)
        {
            var splittedData = description.Split('-')[0].Split(' ');
            bool isExists;
            int walletNumber;

            if (splittedData.Length > 2)
            {
                isExists = userModel?.Any(x => x.WalletNumber.ToString() == splittedData[2]) ?? false;
                walletNumber = 270573;//Convert.ToInt32(splittedData[2]);
            }
            else
            {
                isExists = userModel?.Any(x => x.WalletNumber.ToString() == splittedData[1]) ?? false;
                walletNumber = 270573;//Convert.ToInt32(splittedData[1]);
            }

            return (isExists, walletNumber);

        }

        

    }
}