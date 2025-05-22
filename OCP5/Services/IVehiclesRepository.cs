using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services;

public interface IVehiclesRepository : IRepository<Vehicle>;

public class VehiclesRepository(ApplicationDbContext context) : Repository<Vehicle>(context), IVehiclesRepository
{
    public override async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear).ToArrayAsync();
    }
    
    public override IEnumerable<Vehicle> GetAll()
    {
        return Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear);
    }
    
    public override async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear)
            .SingleOrDefaultAsync(s => s.Id == id);
    }
    
    public override Vehicle? GetById(int id)
    {
        return Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear)
            .SingleOrDefault(s => s.Id == id);
    }
}