using MicroRabbit.Transfer.Data.EntityConfigurations;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public TransferDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TransferLog> TransferLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(new TransferLogEntityTypeConfiguration().GetType().Assembly);
        }

    }
}