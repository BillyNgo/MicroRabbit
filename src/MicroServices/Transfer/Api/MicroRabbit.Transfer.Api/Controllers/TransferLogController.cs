using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Transfer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferLogController : ControllerBase
    {
        private readonly ILogger<TransferLogController> _logger;
        private readonly IMediator _mediator;
        private readonly ITransferService _transferService;
        public TransferLogController(IMediator mediator, ILogger<TransferLogController> logger, ITransferService transferService)
        {
            _mediator = mediator;
            _logger = logger;
            _transferService = transferService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransferLogViewModel>>> Get()
        {
            var results = await _transferService.GetTransferLogs();
            return Ok(results);
        }
    }
}
