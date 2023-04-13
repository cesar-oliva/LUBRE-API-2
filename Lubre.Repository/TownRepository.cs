using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class TownRepository : ITownRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public TownRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseTownRequestDTO> AddAsync(RegisterTownRequestDTO town)
    {
        var newTown = _mapper.Map<Address>(town);
        await _dbc.AddAsync(newTown);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newTown).State = EntityState.Unchanged;
        return _mapper.Map<ResponseTownRequestDTO>(newTown);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Towns.Find(id);
        if (tmp != null)
        {
            _dbc.Towns.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseTownRequestDTO>> GetAllAsync()
    {
        List<ResponseTownRequestDTO> TownDTO = new();
        var ListTown = await _dbc.Towns
                            .Include(c => c.Cities)
                            .ToListAsync();

        foreach (var item in ListTown)
        {
            var newTownDto = _mapper.Map<ResponseTownRequestDTO>(item);
            TownDTO.Add(newTownDto);
        } 
        return TownDTO;
    }

    public async Task<ResponseTownRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Towns.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseTownRequestDTO>(tmp);
    }

    public async Task<ResponseTownRequestDTO> UpdateAsync(Guid id,RegisterTownRequestDTO town)
    {
        if (town == null) return null;
        var newTown = _mapper.Map<Town>(town);
        newTown.Id = id;
        _dbc.Towns.Update(newTown);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponseTownRequestDTO>(newTown);
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

