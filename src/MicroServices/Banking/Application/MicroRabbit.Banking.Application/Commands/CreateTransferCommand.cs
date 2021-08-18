using MicroRabbit.Domain.Core.Commands;

namespace MicroRabbit.Banking.Application.Commands
{
    public class CreateTransferCommand : Command
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }

        public CreateTransferCommand(int fromAccount, int toAccount, decimal transferAmount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }
    }
}