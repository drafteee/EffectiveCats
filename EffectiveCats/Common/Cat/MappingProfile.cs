using AutoMapper;
using MediatR.Cat.Responses;

namespace MediatR.MCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Cat, GetByIdCatResponse>();
            CreateMap<Domain.Entities.Cat, GetAllCatResponse>();
            CreateMap<UpdateCat.Command, Domain.Entities.Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    if (source.Image != null)
                    {
                        source.Image.CopyTo(stream);
                        return stream.ToArray();
                    }
                    return null;
                }));
            CreateMap<CreateCat.Command, Domain.Entities.Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    source.Image.CopyTo(stream);
                    return stream.ToArray();
                }));
        }
    }
}
