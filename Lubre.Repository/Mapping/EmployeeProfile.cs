using AutoMapper;
using Lubre.Entities;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Mapping;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<RegisterEmployeeRequestDTO, Employee>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => true));

        CreateMap<Employee, ResponseEmployeeRequestDTO>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.Name+ " " + src.LastName))
            .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => DateTime.Now.Year-src.DateOfBirth.Year))
            .ForMember(
                dest => dest.Antiquity,
                opt => opt.MapFrom(src => DateTime.Now.Year-src.StartDate.Year))
            .ForMember(
                dest => dest.UnitName,
                opt => opt.MapFrom(src => src.Unit.Name))
            .ForMember(
                dest => dest.GenderName,
                opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(
                dest => dest.PositionName,
                opt => opt.MapFrom(src => src.Position.Name));                     
            
        // en el caso de que los nombres no coincidan con el DTO - dominio
        //CreateMap<EmployeeDTO, Employee>()
        //  .ForMember( d => d.EmployeeFirstName, 
        //              o => o.MapFrom(
        //              s => s.PrimerNombre));   

    }
}
