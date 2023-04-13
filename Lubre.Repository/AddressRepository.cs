using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class AddressRepository : IAddressRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public AddressRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseAddressRequestDTO> AddAsync(RegisterAddressRequestDTO address)
    {
        var newAddress = _mapper.Map<Address>(address);
        await _dbc.AddAsync(newAddress);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newAddress).State = EntityState.Unchanged;
        return _mapper.Map<ResponseAddressRequestDTO>(newAddress);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Addresses.Find(id);
        if (tmp != null)
        {
            _dbc.Addresses.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseAddressRequestDTO>> GetAllAsync()
    {
        List<ResponseAddressRequestDTO> AddressDTO = new();
        var ListAddress = await _dbc.Addresses
                            .Include(c => c.City)
                            .ToListAsync();

        foreach (var item in ListAddress)
        {
            var newAddressDto = _mapper.Map<ResponseAddressRequestDTO>(item);
            AddressDTO.Add(newAddressDto);
        } 
        return AddressDTO;
    }

    public async Task<ResponseAddressRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Addresses.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseAddressRequestDTO>(tmp);
    }

    public async Task<ResponseAddressRequestDTO> UpdateAsync(Guid id,RegisterAddressRequestDTO address)
    {
        if (address == null) return null;
        var newAddress = _mapper.Map<Address>(address);
        newAddress.Id = id;
        _dbc.Addresses.Update(newAddress);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponseAddressRequestDTO>(newAddress);
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

