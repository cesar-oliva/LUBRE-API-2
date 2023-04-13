

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IPositionRepository: IDisposable
{
    Task<IEnumerable<ResponsePositionRequestDTO>> GetAllAsync();
    Task<ResponsePositionRequestDTO> GetByIdAsync(Guid id);
    Task<ResponsePositionRequestDTO> AddAsync(RegisterPositionRequestDTO positionDTO);
    Task<ResponsePositionRequestDTO> UpdateAsync(Guid id,RegisterPositionRequestDTO positionDTO);
    void DeleteAsync(Guid id);
}

