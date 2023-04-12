using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class EmployeeRepository : IEmployeeRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public EmployeeRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseEmployeeRequestDTO> AddAsync(RegisterEmployeeRequestDTO employee)
    {
        var newEmployee = _mapper.Map<Employee>(employee);
        await _dbc.AddAsync(employee);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(employee).State = EntityState.Unchanged;
        return _mapper.Map<ResponseEmployeeRequestDTO>(employee);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Employees.Find(id);
        if (tmp != null)
        {
            _dbc.Employees.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseEmployeeRequestDTO>> GetAllAsync()
    {
        List<ResponseEmployeeRequestDTO> employeeDTO = new();
        var ListEmployee = await _dbc.Employees
                            .Include(u => u.Unit)
                            .Include(p => p.Position)  
                            .Include(g => g.Gender)                        
                            .ToListAsync();

        foreach (var item in ListEmployee)
        {
            var newEmployeeDto = _mapper.Map<ResponseEmployeeRequestDTO>(item);
            employeeDTO.Add(newEmployeeDto);
        } 
        return employeeDTO;
    }

    public async Task<ResponseEmployeeRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Employees.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseEmployeeRequestDTO>(tmp);
    }

    public async Task<ResponseEmployeeRequestDTO> UpdateAsync(Guid id,RegisterEmployeeRequestDTO employee)
    {
        if (employee == null) return null;
        _dbc.Employees.Update(_mapper.Map<Employee>(employee));
        await _dbc.SaveChangesAsync();
        _dbc.Entry(employee).State = EntityState.Unchanged;
        return _mapper.Map<ResponseEmployeeRequestDTO>(employee);
    }


    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbc.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

