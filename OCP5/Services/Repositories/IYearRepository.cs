using Microsoft.AspNetCore.Mvc.Rendering;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IYearRepository : IRepository<VehicleYear>
{
    Task<SelectList> GetSelectListAsync();
}

public class YearRepository(ApplicationDbContext context) : Repository<VehicleYear>(context), IYearRepository
{
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(VehicleYear.Id), nameof(VehicleYear.Year));
    }
}