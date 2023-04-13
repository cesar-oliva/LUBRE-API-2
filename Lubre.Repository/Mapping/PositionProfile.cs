using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<RegisterPositionRequestDTO, Position>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.PositionName));

        CreateMap<Position, ResponsePositionRequestDTO>()
            .ForMember(
                dest => dest.PositionName,
                opt => opt.MapFrom(src => src.Name)
            );     
        CreateMap<ResponsePositionRequestDTO,Position>()
        .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.PositionName));                 
    }
}
