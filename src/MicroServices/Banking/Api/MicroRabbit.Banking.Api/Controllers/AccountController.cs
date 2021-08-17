using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMediator _mediator;
        public AccountController(IAccountService accountService, IMediator mediator, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _mediator = mediator;
            _logger = logger;
        }

        // GET api/banking
        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            var results = await _accountService.GetAccounts();
            return Ok(results);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] TransferViewModel accountTransfer)
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }
        
    }
}