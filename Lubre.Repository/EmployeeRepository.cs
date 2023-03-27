using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public EmployeeRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public Task<ResponseEmployeeRequestDTO> AddAsync(ResponseEmployeeRequestDTO employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ResponseEmployeeRequestDTO>> GetAllAsync()
    {
        List<ResponseEmployeeRequestDTO> employeeDTO = new();

        var ListEmployee = await _dbc.Employees
                            .Include(u => u.Unit)
                            .Include(p => p.Position)
                            .Include(d => d.Documents)
                            .Include(g => g.Gender)
                            .ToListAsync();

        foreach (var item in ListEmployee)
        {
            var newEmployeeDto = _mapper.Map<ResponseEmployeeRequestDTO>(item);
            employeeDTO.Add(newEmployeeDto);
        }    
        return employeeDTO;
    }

    public Task<ResponseEmployeeRequestDTO> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseEmployeeRequestDTO> UpdateAsync(ResponseEmployeeRequestDTO employee)
    {
        throw new NotImplementedException();
    }
}

