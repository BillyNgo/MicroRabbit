using AutoMapper;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Transfer.Application.Models;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Api.Infrastructure.Mapper
{
    public class ApiAutoMappingProfile : Profile
    {
        public ApiAutoMappingProfile()
        {
            CreateMap<TransferLog, TransferLogViewModel>();
            CreateMap<TransferLogViewModel, TransferLog>();
        }
    }
}
