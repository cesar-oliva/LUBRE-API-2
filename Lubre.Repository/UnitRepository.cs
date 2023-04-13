using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class UnitRepository : IUnitRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public UnitRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseUnitRequestDTO> AddAsync(RegisterUnitRequestDTO unit)
    {
        var newUnit = _mapper.Map<Unit>(unit);
        await _dbc.AddAsync(newUnit);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newUnit).State = EntityState.Unchanged;
        return _mapper.Map<ResponseUnitRequestDTO>(newUnit);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Units.Find(id);
        if (tmp != null)
        {
            _dbc.Units.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseUnitRequestDTO>> GetAllAsync()
    {
        List<ResponseUnitRequestDTO> unitDTO = new();
        var ListUnit = await _dbc.Units.ToListAsync();

        foreach (var item in ListUnit)
        {
            var newUnitDto = _mapper.Map<ResponseUnitRequestDTO>(item);
            unitDTO.Add(newUnitDto);
        } 
        return unitDTO;
    }

    public async Task<ResponseUnitRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Units.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseUnitRequestDTO>(tmp);
    }

    public async Task<ResponseUnitRequestDTO> UpdateAsync(Guid id,RegisterUnitRequestDTO unit)
    {
        if (unit == null) return null;
        var newUnit = _mapper.Map<Unit>(unit);
        newUnit.Id = id;
        _dbc.Units.Update(newUnit);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponseUnitRequestDTO>(newUnit);
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

