using System.Collections.Generic;
using MediatR;
using MicroRabbit.Transfer.Application.Models;

namespace MicroRabbit.Transfer.Application.Queries
{
    /// <summary>
    /// Regular query command
    /// </summary>
    public class GetAllTransferLogQuery : IRequest<List<TransferLogDto>>
    {
    }

}
