using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly IEventBus _bus;
        private readonly ITransferLogRepository _transferLogRepository;

        public TransferService(IEventBus bus, ITransferLogRepository transferLogRepository)
        {
            _bus = bus;
            _transferLogRepository = transferLogRepository;
        }

        public async Task<List<TransferLog>> GetTransferLogs()
        {
            return await _transferLogRepository.GetTransferLogs();
        }
    }
}