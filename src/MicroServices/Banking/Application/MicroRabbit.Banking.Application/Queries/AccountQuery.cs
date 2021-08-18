using System.Collections.Generic;
using MediatR;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Domain.Core.Commands;

namespace MicroRabbit.Banking.Application.Queries
{
    public class AccountQuery : IRequest<List<AccountViewModel>>
    {
    }
}