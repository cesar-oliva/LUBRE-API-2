using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class GenderProfile : Profile
{
    public GenderProfile()
    {
        CreateMap<RegisterGenderRequestDTO, Gender>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.GenderName));

        CreateMap<Gender, ResponseGenderRequestDTO>()
            .ForMember(
                dest => dest.GenderName,
                opt => opt.MapFrom(src => src.Name)
            );                     
    }
}
