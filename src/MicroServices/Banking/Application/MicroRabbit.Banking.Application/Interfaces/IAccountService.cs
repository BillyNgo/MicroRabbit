using System.Collections.Generic;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        List<AccountDto> GetAccounts();
        void Transfer(AccountTransferDto accountTransfer);
    }
}