using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroRabbit.Transfer.Data.EntityConfigurations
{
    class TransferLogEntityTypeConfiguration : IEntityTypeConfiguration<TransferLog>
    {
        public void Configure(EntityTypeBuilder<TransferLog> builder)
        {
            builder.ToTable("TransferLogs", TransferDbContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.FromAccount).IsRequired();
            builder.Property(e => e.ToAccount).IsRequired();
            builder.Property(e => e.TransferAmount).HasPrecision(18, 2).IsRequired();
            builder.Property(e => e.TimeStamps);
            builder.HasKey(x => x.Id);
        }
    }
}
