using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Banking.Application.Events
{
    public class TransferCreatedEvent : Event
    {
        public int FromAccount { get; private set; }
        public int ToAccount { get; private set; }
        public decimal TransferAmount { get; private set; }

        public TransferCreatedEvent(int fromAccount, int toAccount, decimal transferAmount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }
    }
}