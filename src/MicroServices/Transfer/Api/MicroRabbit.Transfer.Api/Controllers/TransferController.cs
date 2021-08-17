using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Transfer.Application.Models;
using MicroRabbit.Transfer.Application.Queries;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Transfer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ILogger<TransferController> _logger;
        private readonly IMediator _mediator;
        public TransferController(IMediator mediator, ILogger<TransferController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransferLogDto>>> Get()
        {
            var results = await _mediator.Send(new GetAllTransferLogQuery());
            return Ok(results);
        }
    }
}
