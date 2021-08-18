using AutoMapper;
using MicroRabbit.Banking.Application.Commands;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Application.Configurations.Mapper
{
    public class ApplicationAutoMappingProfile : Profile
    {
        public ApplicationAutoMappingProfile()
        {
            CreateMap<Account, AccountViewModel>();
            CreateMap<AccountViewModel, Account>();
        }
    }
}
