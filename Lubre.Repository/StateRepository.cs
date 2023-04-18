using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class StateRepository : IStateRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public StateRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseStateRequestDTO> AddAsync(RegisterStateRequestDTO state)
    {
        var newState = _mapper.Map<State>(state);
        await _dbc.AddAsync(newState);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newState).State = EntityState.Unchanged;
        return _mapper.Map<ResponseStateRequestDTO>(newState);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.States.Find(id);
        if (tmp != null)
        {
            _dbc.States.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseStateRequestDTO>> GetAllAsync()
    {
        List<ResponseStateRequestDTO> StatesDTO = new();
        var ListStates = await _dbc.States
                            .Include(c => c.Country)
                            .ToListAsync();

        foreach (var item in ListStates)
        {
            var newStatesDto = _mapper.Map<ResponseStateRequestDTO>(item);
            StatesDTO.Add(newStatesDto);
        } 
        return StatesDTO;
    }

    public async Task<ResponseStateRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.States.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseStateRequestDTO>(tmp);
    }

    public async Task<ResponseStateRequestDTO> UpdateAsync(Guid id,RegisterStateRequestDTO state)
    {
        if (state == null) return null;
        var newState = _mapper.Map<State>(state);
        newState.Id = id;
        _dbc.States.Update(newState);
        await _dbc.SaveChangesAsync();
        return _mapper.Map<ResponseStateRequestDTO>(newState);
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

