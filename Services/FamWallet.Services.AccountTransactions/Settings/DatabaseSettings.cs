namespace FamWallet.Services.AccountTransactions.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string TransactionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
