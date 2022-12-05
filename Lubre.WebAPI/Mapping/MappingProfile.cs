using AutoMapper;
using Lubre.Entities;
using Lubre.WebAPI.DataTransferObject;

namespace Lubre.WebAPI.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterEmployeeRequestDTO, Employee>();
        CreateMap<Employee, RegisterEmployeeRequestDTO>();               
            
        // en el caso de que los nombres no coincidan con el DTO - dominio
        //CreateMap<EmployeeDTO, Employee>()
        //  .ForMember( d => d.EmployeeFirstName, 
        //              o => o.MapFrom(
        //              s => s.PrimerNombre));   

    }
}
