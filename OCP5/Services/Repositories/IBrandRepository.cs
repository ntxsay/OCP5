using Microsoft.AspNetCore.Mvc.Rendering;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    Task<SelectList> GetSelectListAsync();
}

public class BrandRepository(ApplicationDbContext context) : Repository<Brand>(context), IBrandRepository
{
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(Brand.Id), nameof(Brand.Name));
    }
}