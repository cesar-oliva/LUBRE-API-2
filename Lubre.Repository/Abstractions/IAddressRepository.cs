

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IAddressRepository: IDisposable
{
    Task<IEnumerable<ResponseAddressRequestDTO>> GetAllAsync();
    Task<ResponseAddressRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseAddressRequestDTO> AddAsync(RegisterAddressRequestDTO addressDTO);
    Task<ResponseAddressRequestDTO> UpdateAsync(Guid id,RegisterAddressRequestDTO addressDTO);
    void DeleteAsync(Guid id);
}

