using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class StateProfile : Profile
{
    public StateProfile()
    {
        CreateMap<RegisterStateRequestDTO, State>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.StateName));

        CreateMap<State, ResponseStateRequestDTO>()
            .ForMember(
                dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(
                dest => dest.StateName,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<ResponseStateRequestDTO,State>()
            .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.StateName));     
    } 
}
