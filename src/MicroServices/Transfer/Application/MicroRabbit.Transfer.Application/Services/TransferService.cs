using System.Collections.Generic;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Services
{
    public class TransferService:ITransferService
    {
        private readonly IEventBus _bus;
        private readonly ITransferRepository _transferRepository;

        public TransferService(IEventBus bus, ITransferRepository transferRepository)
        {
            _bus = bus;
            _transferRepository = transferRepository;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferRepository.GetTransferLogs();
        }
    }
}