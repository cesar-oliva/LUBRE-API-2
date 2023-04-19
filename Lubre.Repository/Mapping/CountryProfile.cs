using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<RegisterCountryRequestDTO, Country>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.CountryName));

        CreateMap<Country, ResponseCountryRequestDTO>()
            .ForMember(
                dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<ResponseCountryRequestDTO,Country>()
            .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.CountryName))
            .ForMember(
                    dest => dest.States,
                    opt => opt.Ignore());    
    } 
}
