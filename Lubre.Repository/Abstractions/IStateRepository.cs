

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IStateRepository: IDisposable
{
    Task<IEnumerable<ResponseStateRequestDTO>> GetAllAsync();
    Task<ResponseStateRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseStateRequestDTO> AddAsync(RegisterStateRequestDTO stateDTO);
    Task<ResponseStateRequestDTO> UpdateAsync(Guid id,RegisterStateRequestDTO stateDTO);
    void DeleteAsync(Guid id);
}

