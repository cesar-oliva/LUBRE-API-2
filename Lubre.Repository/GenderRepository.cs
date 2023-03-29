using Lubre.DataAccess;
using Lubre.Entities;
using AutoMapper;
using Lubre.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lubre.Repository.DataTransferObject.Outgoing;
using Lubre.Repository.DataTransferObject.Incoming;

namespace Lubre.Repository;

public class GenderRepository : IGenderRepository, IDisposable
{
    private readonly ApplicationDbContext _dbc;
    private readonly IMapper _mapper;

    public GenderRepository(ApplicationDbContext dbc,IMapper mapper)
    {
        _dbc = dbc;
        _mapper = mapper;
    }


    public async Task<ResponseGenderRequestDTO> AddAsync(RegisterGenderRequestDTO gender)
    {
        var newGender = _mapper.Map<Gender>(gender);
        await _dbc.AddAsync(newGender);
        await _dbc.SaveChangesAsync();
        _dbc.Entry(newGender).State = EntityState.Unchanged;
        return _mapper.Map<ResponseGenderRequestDTO>(newGender);
    }

    public void DeleteAsync(Guid id)
    {
        var tmp =  _dbc.Genders.Find(id);
        if (tmp != null)
        {
            _dbc.Genders.Remove(tmp);
            _dbc.SaveChanges();
            _dbc.Entry(tmp).State = EntityState.Unchanged;        
        }
    }

    public async Task<IEnumerable<ResponseGenderRequestDTO>> GetAllAsync()
    {
        List<ResponseGenderRequestDTO> genderDTO = new();
        var ListGender = await _dbc.Genders.ToListAsync();

        foreach (var item in ListGender)
        {
            var newGenderDto = _mapper.Map<ResponseGenderRequestDTO>(item);
            genderDTO.Add(newGenderDto);
        } 
        return genderDTO;
    }

    public async Task<ResponseGenderRequestDTO> GetByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;        
        var tmp = await _dbc.Genders.FindAsync(id);
        if (tmp == null) return null;
        return _mapper.Map<ResponseGenderRequestDTO>(tmp);
    }

    public async Task<ResponseGenderRequestDTO> UpdateAsync(RegisterGenderRequestDTO gender)
    {
        if (gender == null) return null;
        _dbc.Genders.Update(_mapper.Map<Gender>(gender));
        await _dbc.SaveChangesAsync();
        _dbc.Entry(gender).State = EntityState.Unchanged;
        return _mapper.Map<ResponseGenderRequestDTO>(gender);
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

