using System.Collections.Generic;
using MediatR;
using MicroRabbit.Banking.Application.Models;

namespace MicroRabbit.Banking.Application.Queries
{
    /// <summary>
    /// Regular query command
    /// </summary>
    public class GetAllAccountQuery : IRequest<List<AccountDto>>
    {
    }

}
