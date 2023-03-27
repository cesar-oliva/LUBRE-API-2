

using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IEmployeeRepository
{
    Task<IEnumerable<ResponseEmployeeRequestDTO>> GetAllAsync();
    Task<ResponseEmployeeRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseEmployeeRequestDTO> AddAsync(ResponseEmployeeRequestDTO employeeDTO);
    Task<ResponseEmployeeRequestDTO> UpdateAsync(ResponseEmployeeRequestDTO employeeDTO);
    Task DeleteAsync(Guid id);
}

