using AutoMapper;
using Domain.Entities;
using MediatR.TypeCat.Responses;

namespace MediatR.MTypeCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatType, GetByIdTypeCatResponse>();
            CreateMap<CatType, GetAllTypeCatResponse>();
            CreateMap<CreateCatType.Command, CatType>();
            CreateMap<UpdateCatType.Command, CatType>();
        }
    }
}
