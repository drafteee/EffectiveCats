using AutoMapper;
using Common.MCat;
using Common.MTypeCat;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Common.MBase
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCat.Command, Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source,dest) => {
                    var stream = new MemoryStream();
                    source.Image.CopyTo(stream);
                    return stream.ToArray();
                }));
            CreateMap<CreateCatType.Command, CatType>();

            CreateMap<UpdateCat.Command, Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    if(source.Image != null)
                    {
                        source.Image.CopyTo(stream);
                        return stream.ToArray();
                    }
                    return null;
                }));
            CreateMap<UpdateCatType.Command, CatType>();
        }
    }
}
