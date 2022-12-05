using Lubre.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lubre.DataAccess;

public class DbContext<T> : IDbContext<T> where T : class,IEntity
{
    DbSet<T> _items;
    ApplicationDbContext _adbc;

    /// <summary>
    /// builder DbContext
    /// </summary>
    /// <remarks>
    /// We receive by parameters in the constructor the instance of apidbcontext where the data provider is. is injected and 
    /// we set the list and make it available for use
    /// </remarks>
    /// <param name="adbc"></param>
    public DbContext(ApplicationDbContext adbc)
    {
        _adbc = adbc;
        _items = adbc.Set<T>();
    }

    /// <summary>
    /// delete entity
    /// </summary>
    /// <remark> 
    ///we ask item to return the list from the database and we return it through the function
    /// </remark>
    /// <param name="id"></param>

    public void Delete(Guid id)
    {
        var tmp = _items.Find(id);
        if (tmp != null)
        {
            _items.Remove(tmp);
            _adbc.SaveChanges();
            _adbc.Entry(tmp).State = EntityState.Unchanged;
        }
    }
    /// <summary>
    /// delete async entity
    /// </summary>
    /// <param name="id"></param>
    public void DeleteAsync(Guid id)
    {
        var tmp = _items.Find(id);
        if (tmp != null)
        {
            _items.Remove(tmp);
            _adbc.SaveChangesAsync();
            _adbc.Entry(tmp).State = EntityState.Unchanged;
        }
    }

    /// <summary>
    /// get all entity
    /// </summary>
    /// <returns>entity</returns>
    public IList<T> GetAll()
    {
        var tmp = _items.AsNoTracking().ToList();
        return tmp;
    }

    /// <summary>
    /// get all entity async
    /// </summary>
    /// <returns>entity</returns>
    public async Task<IList<T>> GetAllAsync()
    {
        var tmp = await _items.AsNoTracking().ToListAsync();
        return tmp;
    }

    /// <summary>
    ///  get entity by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>entity</returns>
    public T GetById(Guid id)
    {
        var tmp = _items.Find(id);
        _adbc.Entry(tmp).State = EntityState.Unchanged;
        return tmp;
    }

    /// <summary>
    ///  get entity by Id async
    /// </summary>
    /// <param name="id"></param>
    /// <returns>entity</returns>
    public async Task<T> GetByIdAsync(Guid id)
    {
        var tmp = await _items.FindAsync(id);
        _adbc.Entry(tmp).State = EntityState.Unchanged;
        return tmp;
    }

    /// <summary>
    /// save entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns> entity</returns>
    public T Save(T entity)
    {
        if (entity.Id.Equals(Guid.Empty))
        {
            _items.Add(entity);
        }
        else
        {
            _items.Update(entity);
        }
        _adbc.SaveChanges();
        _adbc.Entry(entity).State = EntityState.Unchanged;
        return entity;
    }

    /// <summary>
    /// save entity async
    /// </summary>
    /// <param name="entity"></param>
    /// <returns> entity</returns>
    public async Task<T> SaveAsync(T entity)
    {
        await _items.AddAsync(entity);
        await _adbc.SaveChangesAsync();
        _adbc.Entry(entity).State = EntityState.Unchanged;
        return entity;
    }

    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>entity</returns>
    public T Update(T entity)
    {

        _items.Update(entity);
        _adbc.SaveChanges();
        _adbc.Entry(entity).State = EntityState.Unchanged;
        return entity;
    }

    /// <summary>
    /// update entity async
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>entity</returns>
    public async Task<T> UpdateAsync(T entity)
    {
        _items.Update(entity);
        await _adbc.SaveChangesAsync();
        _adbc.Entry(entity).State = EntityState.Unchanged;
        return entity;
    }	
}
    