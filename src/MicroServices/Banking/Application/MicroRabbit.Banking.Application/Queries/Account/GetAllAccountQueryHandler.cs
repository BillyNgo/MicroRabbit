using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Banking.Application.Queries
{
    /// <summary>
    /// Regular query command
    /// </summary>
    public class GetAllAccountQueryHandler : IRequestHandler<GetAllAccountQuery, List<AccountDto>>
    {
        private readonly ILogger<GetAllAccountQueryHandler> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _bus;
        public GetAllAccountQueryHandler(IAccountRepository accountRepository, IEventBus bus, IMapper mapper, ILogger<GetAllAccountQueryHandler> logger)
        {
            _accountRepository = accountRepository;
            _bus = bus;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AccountDto>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Executing account query process.");
                var accountsList = await _accountRepository.GetAccounts();
                var accountListDto = _mapper.Map<List<Account>, List<AccountDto>>(accountsList);
                return accountListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
 
        }
    }

}
