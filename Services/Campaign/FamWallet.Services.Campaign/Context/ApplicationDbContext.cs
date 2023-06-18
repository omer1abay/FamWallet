using FamWallet.Services.Campaign.Models;
using Microsoft.EntityFrameworkCore;


namespace FamWallet.Services.Campaign.Context
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DiscountModel>();
        }

        //public DbSet<DiscountModel> Discount { get; set; }

    }
}
