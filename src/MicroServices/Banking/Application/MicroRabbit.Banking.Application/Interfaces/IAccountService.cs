using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAccounts();
        void Transfer(AccountTransferDto accountTransfer);
    }
}