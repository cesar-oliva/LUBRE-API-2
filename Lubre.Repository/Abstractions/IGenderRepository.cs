

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface IGenderRepository: IDisposable
{
    Task<IEnumerable<ResponseGenderRequestDTO>> GetAllAsync();
    Task<ResponseGenderRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseGenderRequestDTO> AddAsync(RegisterGenderRequestDTO genderDTO);
    Task<ResponseGenderRequestDTO> UpdateAsync(RegisterGenderRequestDTO genderDTO);
    void DeleteAsync(Guid id);
}

