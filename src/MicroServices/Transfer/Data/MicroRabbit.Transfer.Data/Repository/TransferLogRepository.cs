using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferLogRepository : ITransferLogRepository
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

        public Task<List<TransferLog>> GetTransferLogs()
        {
            return _dbContext.TransferLogs.ToListAsync();
        }

        
    }
}