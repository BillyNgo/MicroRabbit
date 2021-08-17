using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Commands
{
    public class TransferCreatedEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferLogRepository _transferLogRepository;
        public TransferCreatedEventHandler(ITransferLogRepository transferLogRepository)
        {
            _transferLogRepository = transferLogRepository;
        }
        public Task Handle(TransferCreatedEvent @event)
        {
            _transferLogRepository.Add(new TransferLog()
            {
                FromAccount = @event.FromAccount,
                ToAccount = @event.ToAccount,
                TransferAmount = @event.TransferAmount
            });
            return Task.CompletedTask;
        }
    }
}