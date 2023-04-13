using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<RegisterUnitRequestDTO, Unit>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.UnitName));

        CreateMap<Unit, ResponseUnitRequestDTO>()
            .ForMember(
                dest => dest.UnitName,
                opt => opt.MapFrom(src => src.Name)
            );     
        CreateMap<ResponseUnitRequestDTO,Unit>()
        .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.UnitName));                 
    }
}
