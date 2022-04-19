using AutoMapper;
using Common.MCat.Dto;
using DAL.Models;

namespace Common.MCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cat, GetByIdCatDto>();
            CreateMap<Cat, GetAllCatDto>();
            CreateMap<UpdateCat.Command, Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    if (source.Image != null)
                    {
                        source.Image.CopyTo(stream);
                        return stream.ToArray();
                    }
                    return null;
                }));
            CreateMap<CreateCat.Command, Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    source.Image.CopyTo(stream);
                    return stream.ToArray();
                }));
        }
    }
}
