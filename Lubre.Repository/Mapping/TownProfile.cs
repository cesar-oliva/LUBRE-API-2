using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class TownProfile : Profile
{
    public TownProfile()
    {
        CreateMap<RegisterTownRequestDTO, Town>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.TownName));

        CreateMap<Town, ResponseTownRequestDTO>()
            .ForMember(
                dest => dest.TownName,
                opt => opt.MapFrom(src => src.Name)
            );     
        CreateMap<ResponseTownRequestDTO,Town>()
        .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.TownName));                 
    }
}
