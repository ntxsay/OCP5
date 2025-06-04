using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;

namespace OCP5.Services.Repositories;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// Récupère un élément par son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T? GetById(int id);
    
    /// <summary>
    /// Récupère un élément par son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(int id);
    
    /// <summary>
    /// Récupère tous les éléments.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> GetAll();
    
    /// <summary>
    /// Récupère tous les éléments de manière asynchrone.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync();
    
    public void Add(T entity);
    public Task AddAsync(T entity);
    public void AddRange(IEnumerable<T> entities);
    public Task AddRangeAsync(IEnumerable<T> entities);
    public void Remove(T entity);
    public void RemoveRange(IEnumerable<T> entities);
    public void SaveChanges();
    public Task SaveChangesAsync();
}

public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext Context = context;

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual T? GetById(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
    
    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}