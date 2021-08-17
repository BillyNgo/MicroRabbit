using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Domain.Core.Commands;

namespace MicroRabbit.Banking.Application.Commands
{
    public class CreateTransferCommand: AccountTransferDto
    {
        public CreateTransferCommand(int fromAccount, int toAccount, decimal transferAmount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }
    }
}