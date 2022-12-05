using Lubre.Abstractions;

namespace Lubre.Repository;

public class Repository<T> : IRepository<T> where T : IEntity
{
    IDbContext<T> _dbc;
    public Repository(IDbContext<T> dbc)
    {
        _dbc = dbc;
    }

    public void Delete(Guid id)
    {
        _dbc.Delete(id);
    }
    public void DeleteAsync(Guid id)
    {
        _dbc.DeleteAsync(id);
    }

    public IList<T> GetAll()
    {
        return _dbc.GetAll();
    }
    public async Task<IList<T>> GetAllAsync()
    {
        return await _dbc.GetAllAsync();
    }
    public T GetById(Guid id)
    {
        return _dbc.GetById(id);
    }
    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbc.GetByIdAsync(id);
    }
    public T Save(T entity)
    {
        return _dbc.Save(entity);   
    }
    public Task<T> SaveAsync(T entity)
    {
        return _dbc.SaveAsync(entity);
    }

    public T Update(T entity)
    {
        return _dbc.Update(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        return _dbc.UpdateAsync(entity);
    }
}

