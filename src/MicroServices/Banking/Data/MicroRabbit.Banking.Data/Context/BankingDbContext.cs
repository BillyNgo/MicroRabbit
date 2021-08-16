using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(b => b.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Balance).HasPrecision(18, 2);
                entity.HasKey(x => x.Id);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}