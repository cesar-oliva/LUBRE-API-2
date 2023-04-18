

using Lubre.Abstractions;
using Lubre.Repository.DataTransferObject.Incoming;
using Lubre.Repository.DataTransferObject.Outgoing;

namespace Lubre.Repository.Abstractions;
public interface ICountryRepository: IDisposable
{
    Task<IEnumerable<ResponseCountryRequestDTO>> GetAllAsync();
    Task<ResponseCountryRequestDTO> GetByIdAsync(Guid id);
    Task<ResponseCountryRequestDTO> AddAsync(RegisterCountryRequestDTO countryDTO);
    Task<ResponseCountryRequestDTO> UpdateAsync(Guid id,RegisterCountryRequestDTO countryDTO);
    void DeleteAsync(Guid id);
}

