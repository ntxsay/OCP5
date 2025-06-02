using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IModelRepository : IRepository<Model>
{
    Task<IEnumerable<Model>> GetAllAsync(int brandId);
    Task<SelectList> GetSelectListAsync();
    Task<SelectList> GetSelectListAsync(int brandId);
}

public class ModelRepository(ApplicationDbContext context) : Repository<Model>(context), IModelRepository
{
    public virtual async Task<IEnumerable<Model>> GetAllAsync(int brandId)
    {
        return await Context.Set<Model>().Where(m => m.IdBrand == brandId).ToArrayAsync();
    }
    public async Task<SelectList> GetSelectListAsync(int brandId)
    {
        return new SelectList(await GetAllAsync(brandId), nameof(Model.Id), nameof(Model.Name));
    }
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(Model.Id), nameof(Model.Name));
    }
}