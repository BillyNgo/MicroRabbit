using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.Interfaces
{
    public interface ITransferLogRepository
    {
        Task<List<TransferLog>> GetTransferLogs();
        void Add(TransferLog transferLog);
    }
}