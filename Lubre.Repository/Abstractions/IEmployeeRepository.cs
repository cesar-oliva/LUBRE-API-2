

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IEmployeeRepository: IDisposable
{
    Task<IEnumerable<ResponseEmployeeRequestDTO>> GetAllAsync();
    Task<ResponseEmployeeRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseEmployeeRequestDTO> AddAsync(RegisterEmployeeRequestDTO employeeDTO);
    Task<ResponseEmployeeRequestDTO> UpdateAsync(Guid id,RegisterEmployeeRequestDTO employeeDTO);
    void DeleteAsync(Guid id);
}

