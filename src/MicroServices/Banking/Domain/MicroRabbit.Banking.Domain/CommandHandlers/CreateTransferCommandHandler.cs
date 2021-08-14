using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand,bool>
    {
        private readonly IEventBus _bus;

        public CreateTransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMq
            _bus.Publish(new TransferCreatedEvent(request.From,request.To,request.Amount));

            return Task.FromResult(true);
        }
    }
}