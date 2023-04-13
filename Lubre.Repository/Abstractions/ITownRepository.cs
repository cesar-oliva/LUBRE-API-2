

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface ITownRepository: IDisposable
{
    Task<IEnumerable<ResponseTownRequestDTO>> GetAllAsync();
    Task<ResponseTownRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseTownRequestDTO> AddAsync(RegisterTownRequestDTO townDTO);
    Task<ResponseTownRequestDTO> UpdateAsync(Guid id,RegisterTownRequestDTO townDTO);
    void DeleteAsync(Guid id);
}

