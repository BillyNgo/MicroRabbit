using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MicroRabbit.Banking.Application.Commands;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus bus, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<List<AccountDto>> GetAccounts()
        {
            var accountsList = await _accountRepository.GetAccounts();
            var accountListDto = _mapper.Map<List<Account>, List<AccountDto>>(accountsList);
            return accountListDto;
        }

        public void Transfer(AccountTransferDto accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
                accountTransfer.FromAccount,
                accountTransfer.ToAccount,
                accountTransfer.TransferAmount);

            _bus.SendCommand(createTransferCommand);
        }
    }
}