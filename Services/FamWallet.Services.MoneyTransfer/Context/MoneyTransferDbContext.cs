using Microsoft.EntityFrameworkCore;

namespace FamWallet.Services.MoneyTransfer.Context
{
    public class MoneyTransferDbContext:DbContext
    {
        public MoneyTransferDbContext()
        {

        }

        public MoneyTransferDbContext(DbContextOptions<MoneyTransferDbContext> options) : base(options)
        {

        }

    }
}
