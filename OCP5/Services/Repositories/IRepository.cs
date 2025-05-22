using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;

namespace OCP5.Services.Repositories;

public interface IRepository<T> where T : class
{
    public T? GetById(int id);
    public Task<T?> GetByIdAsync(int id);
    public IEnumerable<T> GetAll();
    public Task<IEnumerable<T>> GetAllAsync();
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression);
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

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression);
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