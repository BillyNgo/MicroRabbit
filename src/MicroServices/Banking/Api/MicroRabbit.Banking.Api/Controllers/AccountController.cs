using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator, ILogger<AccountController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        // GET api/banking
        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            var result = await _mediator.Send(new AccountQuery());
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<bool> Post([FromBody] TransferViewModel accountTransfer)
        {
            var command = _mapper.Map<TransferViewModel, CreateTransferCommand>(accountTransfer);
            var result = await _mediator.Send(command);
            return result;
        }
        
    }
}