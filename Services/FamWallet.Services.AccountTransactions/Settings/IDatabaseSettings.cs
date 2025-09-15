namespace FamWallet.Services.AccountTransactions.Settings
{
    public interface IDatabaseSettings
    {
        public string TransactionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
