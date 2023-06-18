using AutoMapper;
using FamWallet.Services.AccountTransactions.DTOs;
using FamWallet.Services.AccountTransactions.Models;
using FamWallet.Services.AccountTransactions.Settings;
using FamWallet.Shared.DTOs;
using MongoDB.Driver;

namespace FamWallet.Services.AccountTransactions.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<TransactionsModel> _transactionCollection;
        private readonly IMapper _mapper;

        public TransactionService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _transactionCollection = database.GetCollection<TransactionsModel>(settings.TransactionCollectionName);
            _mapper = mapper;
        }

        public async void AddTransaction(TransactionDto transaction)
        {
            var responseMap = _mapper.Map<TransactionsModel>(transaction);
            await _transactionCollection.InsertOneAsync(responseMap);
        }

        public async Task<ResponseDto<List<TransactionDto>>> GetTransaction(string walletId)
        {
            var result = await _transactionCollection.Find<TransactionsModel>(x => x.SenderWalletId == walletId || x.ReceiverWalletId == walletId).ToListAsync();
            if (result == null || result.Count == 0)
            {
                return ResponseDto<List<TransactionDto>>.Failure("Hareket Bulunamadı", 400);
            }
            return ResponseDto<List<TransactionDto>>.Success(_mapper.Map<List<TransactionDto>>(result),200);
        }
    }
}
