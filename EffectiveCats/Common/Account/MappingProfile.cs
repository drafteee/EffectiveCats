using AutoMapper;
using DAL.Models.Account;

namespace Common.Account
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Register.Command, User>()
                .ForMember(x=> x.SecurityStamp, y=> y.MapFrom(z=> Guid.NewGuid().ToString()));
        }
    }
}
