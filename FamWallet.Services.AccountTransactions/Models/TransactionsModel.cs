using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FamWallet.Services.AccountTransactions.Models
{
    public class TransactionsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReceiverWalletId { get; set; }
        public string SenderWalletId { get; set; }
        public string Balance { get; set; }
        public string Description { get; set; }
    }
}
