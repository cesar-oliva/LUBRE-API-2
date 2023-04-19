using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class CountryRepository : ICountryRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public CountryRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseCountryRequestDTO> AddAsync(RegisterCountryRequestDTO country)
    {
        var newCountry = _mapper.Map<Country>(country);
        await _dbc.AddAsync(newCountry);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newCountry).State = EntityState.Unchanged;
        return _mapper.Map<ResponseCountryRequestDTO>(newCountry);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Countries.Find(id);
        if (tmp != null)
        {
            _dbc.Countries.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseCountryRequestDTO>> GetAllAsync()
    {
        List<ResponseCountryRequestDTO> CountryDTO = new();

        var ListCountries = await _dbc.Countries
                            .Include(s => s.States)
                            .ToListAsync();
                            
        foreach (var item in ListCountries)
        {
            var newCountriesDto = _mapper.Map<ResponseCountryRequestDTO>(item);
            CountryDTO.Add(newCountriesDto);
        } 
        return CountryDTO;
    }

    public async Task<ResponseCountryRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Countries.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseCountryRequestDTO>(tmp);
    }

    public async Task<ResponseCountryRequestDTO> UpdateAsync(Guid id,RegisterCountryRequestDTO country)
    {
        if (country == null) return null;
        var newCountry = _mapper.Map<Country>(country);
        newCountry.Id = id;
        _dbc.Countries.Update(newCountry);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponseCountryRequestDTO>(newCountry);
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

