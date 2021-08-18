using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Application.Commands;
using MicroRabbit.Banking.Application.Events;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Application.CommandHandlers
{
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public CreateTransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
        {
            //publish event to RabbitMq
            _bus.Publish(new TransferCreatedEvent(command.FromAccount, command.ToAccount, command.TransferAmount));

            return Task.FromResult(true);
        }
    }
}