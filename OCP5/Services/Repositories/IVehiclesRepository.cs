using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Extensions;
using OCP5.Models.Entities;
using OCP5.Models.ViewModels;

namespace OCP5.Services.Repositories;

public interface IVehiclesRepository : IRepository<Vehicle>
{
    public Task SaveDataAsync(VehicleViewModel viewModel);
    public Task<VehicleViewModel?> GetViewModelByIdAsync(int id, bool addSelectList = true);
    public void UpdateData(VehicleViewModel viewModel);
    public Task UpdateDataAsync(VehicleViewModel viewModel);
    public Task<bool> ExistsAsync(int id);
    public Task<VehicleViewModel> EmptyViewModelAsync();
}

public class VehiclesRepository(ApplicationDbContext context, IYearRepository yearRepository, IBrandRepository brandRepository, IModelRepository modelRepository, IFinitionRepository finitionRepository) : Repository<Vehicle>(context), IVehiclesRepository
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
    
    public async Task<VehicleViewModel?> GetViewModelByIdAsync(int id, bool addSelectList = true)
    {
        var model = await GetByIdAsync(id);
        if (model == null)
            return null;
        
        var viewModel = model.ConvertToViewModel();
        if (addSelectList)
        {
            viewModel.Brands = await brandRepository.GetSelectListAsync();
            viewModel.Models = await modelRepository.GetSelectListAsync();
            viewModel.VehicleYears = await yearRepository.GetSelectListAsync();
            viewModel.Finitions = await finitionRepository.GetSelectListAsync();
        }
        
        return viewModel;
    }
    
    public async Task<VehicleViewModel> EmptyViewModelAsync()
    {
        var brands = await brandRepository.GetSelectListAsync();
        var models = await modelRepository.GetSelectListAsync();
        var years = await yearRepository.GetSelectListAsync();
        var finitions = await finitionRepository.GetSelectListAsync();
        var viewModel = new VehicleViewModel()
        {
            Brands = brands,
            Models = models,
            VehicleYears = years,
            Finitions = finitions
        } ;

        return viewModel;
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

    public async Task SaveDataAsync(VehicleViewModel viewModel)
    {
        var model = viewModel.ConvertToModel();
        await Context.Vehicles.AddAsync(model);
        await SaveChangesAsync();
    }
    
    public void UpdateData(VehicleViewModel viewModel)
    {
        if (viewModel.Id > 0)
        {
            var model = viewModel.ConvertToModel();
            Context.Vehicles.Update(model);
            Context.SaveChanges();
        }
    }
    
    public async Task UpdateDataAsync(VehicleViewModel viewModel)
    {
        if (viewModel.Id > 0)
        {
            var model = viewModel.ConvertToModel();
            Context.Vehicles.Update(model);
            await Context.SaveChangesAsync();
        }
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await Context.Vehicles.AnyAsync(v => v.Id == id);
    }
}