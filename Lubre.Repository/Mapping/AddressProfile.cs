using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<RegisterAddressRequestDTO, Address>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<Address, ResponseAddressRequestDTO>()
            .ForMember(
                dest => dest.CityName,
                opt => opt.MapFrom(src => src.City.Name))
            .ForMember(
                dest => dest.TownName,
                opt => opt.MapFrom(src => src.City.Town.Name));

        CreateMap<ResponseAddressRequestDTO,Address>();    
    } 
}
