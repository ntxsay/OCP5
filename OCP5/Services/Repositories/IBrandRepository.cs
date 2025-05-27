using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    Task<SelectList> GetSelectListAsync();
    Task<Dictionary<int, string>> GetModelsAsync(int brandId);
}

public class BrandRepository(ApplicationDbContext context) : Repository<Brand>(context), IBrandRepository
{
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(Brand.Id), nameof(Brand.Name));
    }

    public async Task<Dictionary<int, string>> GetModelsAsync(int brandId)
    {
        return await Context.Models.Where(x => x.IdBrand == brandId).ToDictionaryAsync(d => d.Id, d => d.Name);
    }
}