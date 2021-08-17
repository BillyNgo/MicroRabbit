using AutoMapper;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Api.Infrastructure.Mapper
{
    public class ApiAutoMappingProfile : Profile
    {
        public ApiAutoMappingProfile()
        {
            CreateMap<Account, AccountViewModel>();
            CreateMap<AccountViewModel, Account>();
        }
    }
}
