using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Models;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Transfer.Application.Queries
{
    /// <summary>
    /// Regular query command
    /// </summary>
    public class GetAllTransferLogQueryHandler : IRequestHandler<GetAllTransferLogQuery, List<TransferLogDto>>
    {
        private readonly ILogger<GetAllTransferLogQueryHandler> _logger;
        private readonly ITransferLogRepository _transferLogRepository;
        private readonly IMapper _mapper;
        public GetAllTransferLogQueryHandler(ITransferLogRepository transferLogRepository, IMapper mapper, ILogger<GetAllTransferLogQueryHandler> logger)
        {
            _transferLogRepository = transferLogRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TransferLogDto>> Handle(GetAllTransferLogQuery query, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Executing transfer logs query process.");
                var transferLogList = await _transferLogRepository.GetTransferLogs();
                var transferLogListDto = _mapper.Map<List<TransferLog>, List<TransferLogDto>>(transferLogList);
                return transferLogListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }

}
