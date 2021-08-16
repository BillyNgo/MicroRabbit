using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroRabbit.Banking.Data.EntityConfigurations
{
    class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", BankingDbContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Balance).HasPrecision(18, 2);
            builder.HasKey(x => x.Id);
        }
    }
}
