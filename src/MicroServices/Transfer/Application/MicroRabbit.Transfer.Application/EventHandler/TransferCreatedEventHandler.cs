using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Events;
using MicroRabbit.Transfer.Application.Validators;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.EventHandler
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
            var validator = new TransferCreatedEventValidator();
            var result = validator.Validate(@event);
            if (result.IsValid)
            {
                _transferLogRepository.Add(new TransferLog()
                {
                    FromAccount = @event.FromAccount,
                    ToAccount = @event.ToAccount,
                    TransferAmount = @event.TransferAmount,
                    TimeStamps = @event.TimeStamps
                });
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}