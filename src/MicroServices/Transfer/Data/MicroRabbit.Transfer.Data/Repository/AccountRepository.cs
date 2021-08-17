using System.Collections.Generic;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferLogRepository : ITransferRepository
    {
        private readonly TransferDbContext _dbContext;

        public TransferLogRepository(TransferDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TransferLog transferLog)
        {
            _dbContext.TransferLogs.Add(transferLog);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _dbContext.TransferLogs;
        }

        
    }
}