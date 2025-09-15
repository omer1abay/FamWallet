using AutoMapper;
using FamWallet.Services.AccountTransactions.DTOs;
using FamWallet.Services.AccountTransactions.Models;

namespace FamWallet.Services.AccountTransactions.Mapper
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<TransactionDto,TransactionsModel>().ReverseMap();
        }
    }
}
