using AutoMapper;
using Common.MCat.Dto;
using Domain.Models;

namespace Common.MCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cat, GetByIdCatDto>();
            CreateMap<Cat, GetAllCatDto>();
        }
    }
}
