using Lubre.Abstractions;
using Lubre.Repository;
using System;
using System.Collections.Generic;

namespace Lubre.Application;

public class Application<T> : IApplication<T> where T : IEntity
{
    IRepository<T> _repository;
    /// <summary>
    /// application builder
    /// </summary>
    /// <remarks>
    /// The constructor receives a repository of type T and the instantiated dependency is injected.
    /// which is already created. Through the ADAPTER pattern we pass the CRUD operations
    /// </remarks>
    /// <param name="repository"></param>
    public Application(IRepository<T> repository)
    {
        _repository = repository;
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }
    public void DeleteAsync(Guid id)
    {
        _repository.DeleteAsync(id);
    }
    public IList<T> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<IList<T>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }
    public T GetById(Guid id)
    {
        return _repository.GetById(id);
    }
    public Task<T> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }
    public T Save(T entity)
    {
        return _repository.Save(entity);
    }
    public Task<T> SaveAsync(T entity)
    {
        return _repository.SaveAsync(entity);
    }

    public T Update(T entity)
    {
        return _repository.Update(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        return _repository.UpdateAsync(entity);
    }
}


