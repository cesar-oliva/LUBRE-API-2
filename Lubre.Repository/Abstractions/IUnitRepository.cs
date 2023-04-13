

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IUnitRepository: IDisposable
{
    Task<IEnumerable<ResponseUnitRequestDTO>> GetAllAsync();
    Task<ResponseUnitRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseUnitRequestDTO> AddAsync(RegisterUnitRequestDTO unitDTO);
    Task<ResponseUnitRequestDTO> UpdateAsync(Guid id,RegisterUnitRequestDTO unitDTO);
    void DeleteAsync(Guid id);
}

