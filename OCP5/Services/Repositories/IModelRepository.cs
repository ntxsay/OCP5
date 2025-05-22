using Microsoft.AspNetCore.Mvc.Rendering;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IModelRepository : IRepository<Model>
{
    Task<SelectList> GetSelectListAsync();
}

public class ModelRepository(ApplicationDbContext context) : Repository<Model>(context), IModelRepository
{
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(Model.Id), nameof(Model.Name));
    }
}