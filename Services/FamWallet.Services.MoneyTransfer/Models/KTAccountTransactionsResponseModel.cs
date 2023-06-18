namespace FamWallet.Services.MoneyTransfer.Models
{
    public class KTAccountTransactionsResponseModel
    {
        public string? executionReferenceId { get; set; }
        public Value value { get; set; }
        public bool success { get; set; }
        public string[]? result { get; set; }
    }

    public class Value
    {
        public List<KTAccountActivities>? accountActivities { get; set; }
    }

}
