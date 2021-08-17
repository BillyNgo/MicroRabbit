using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Application.Commands;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Application.Queries;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly ILogger<BankingController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMediator _mediator;
        public BankingController(IAccountService accountService, IMediator mediator, ILogger<BankingController> logger)
        {
            _accountService = accountService;
            _mediator = mediator;
            _logger = logger;
        }

        // GET api/banking
        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            var results = await _mediator.Send(new GetAllAccountQuery());
            return Ok(results);
        }
        
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] AccountTransferDto accountTransfer)
        {
            var results = await _mediator.Send(new CreateTransferCommand(
                fromAccount: accountTransfer.FromAccount,
                toAccount: accountTransfer.ToAccount,
                transferAmount: accountTransfer.TransferAmount
                ));

            return Ok(results);
        }
        
    }
}