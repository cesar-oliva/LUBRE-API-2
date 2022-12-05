namespace Lubre.Abstractions;

/// <summary>
/// Generic interface that defines CRUD operations
/// </summary>
/// <remarks>
/// Save, update or insert as appropriate
/// GetAll, returns a list of entities
/// GetById, returns an object based on its Id
/// Delete, Delete the entity by Id
/// </remarks>
/// <typeparam name="T"></typeparam>

public interface ICrudAsync<T>
{
    Task<T> SaveAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    void DeleteAsync(Guid id);
}
public interface ICrud<T> : ICrudAsync<T>
{
    T Save(T entity);
    T Update(T entity);
    IList<T> GetAll(); 
    T GetById(Guid id); 
    void Delete(Guid id);
} 


