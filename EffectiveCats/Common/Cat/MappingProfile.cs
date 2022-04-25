using AutoMapper;
using MediatR.Cat.Responses;
using Models = Domain.Models;

namespace MediatR.MCat
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Cat, GetByIdCatResponse>();
            CreateMap<Models.Cat, GetAllCatResponse>();
            CreateMap<UpdateCat.Command, Models.Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    if (source.Image != null)
                    {
                        source.Image.CopyTo(stream);
                        return stream.ToArray();
                    }
                    return null;
                }));
            CreateMap<CreateCat.Command, Models.Cat>()
                .ForMember(d => d.Image, opt => opt.MapFrom((source, dest) => {
                    var stream = new MemoryStream();
                    source.Image.CopyTo(stream);
                    return stream.ToArray();
                }));
        }
    }
}
