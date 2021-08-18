using System;
using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Transfer.Application.Events
{
    public class TransferCreatedEvent : Event
    {
        public int FromAccount { get; private set; }
        public int ToAccount { get; private set; }
        public decimal TransferAmount { get; private set; }
        public new DateTime TimeStamps { get; private set; }

        public TransferCreatedEvent(int fromAccount, int toAccount, decimal transferAmount, DateTime timeStamps)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
            TimeStamps = timeStamps;
        }
    }
}