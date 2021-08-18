using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroRabbit.Banking.Application.Commands;
using MicroRabbit.Banking.Application.Events;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Application.Queries;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Application.QueryHandlers
{
    public class AccountQueryHandler : IRequestHandler<AccountQuery, List<AccountViewModel>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<List<AccountViewModel>> Handle(AccountQuery query, CancellationToken cancellationToken)
        {
            var accountsList = await _accountRepository.GetAccounts();
            var accountListDto = _mapper.Map<List<Account>, List<AccountViewModel>>(accountsList);
            return accountListDto;
        }
    }
}