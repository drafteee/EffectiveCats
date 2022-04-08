using AutoMapper;
using Common.MTypeCat.Dto;
using Domain.Models;

namespace Common.MTypeCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatType, GetByIdTypeCatDto>();
            CreateMap<CatType, GetAllTypeCatDto>();
        }
    }
}
