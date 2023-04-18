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
                opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<Country, ResponseCountryRequestDTO>();

        CreateMap<ResponseCountryRequestDTO,Country>();    
    } 
}
