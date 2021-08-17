using MicroRabbit.Domain.Core.Commands;

namespace MicroRabbit.Banking.Application.Models
{
    public class TransferDto : Command
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }

    }
}