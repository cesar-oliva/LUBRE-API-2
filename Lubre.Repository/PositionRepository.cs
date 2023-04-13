using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class PositionRepository : IPositionRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public PositionRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponsePositionRequestDTO> AddAsync(RegisterPositionRequestDTO position)
    {
        var newPosition = _mapper.Map<Position>(position);
        await _dbc.AddAsync(newPosition);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newPosition).State = EntityState.Unchanged;
        return _mapper.Map<ResponsePositionRequestDTO>(newPosition);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Positions.Find(id);
        if (tmp != null)
        {
            _dbc.Positions.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponsePositionRequestDTO>> GetAllAsync()
    {
        List<ResponsePositionRequestDTO> positionDTO = new();
        var ListPosition = await _dbc.Positions.ToListAsync();

        foreach (var item in ListPosition)
        {
            var newPositionDto = _mapper.Map<ResponsePositionRequestDTO>(item);
            positionDTO.Add(newPositionDto);
        } 
        return positionDTO;
    }

    public async Task<ResponsePositionRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Positions.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponsePositionRequestDTO>(tmp);
    }

    public async Task<ResponsePositionRequestDTO> UpdateAsync(Guid id,RegisterPositionRequestDTO position)
    {
        if (position == null) return null;
        var newPosition = _mapper.Map<Position>(position);
        newPosition.Id = id;
        _dbc.Positions.Update(newPosition);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponsePositionRequestDTO>(newPosition);
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

