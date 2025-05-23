using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Extensions;
using OCP5.Models.Entities;
using OCP5.Models.ViewModels;

namespace OCP5.Services.Repositories;

public interface IVehiclesRepository : IRepository<Vehicle>
{
    public Task<VehicleThumbnailViewModel?> GetThumbnailAsync(int id);
    public VehicleThumbnailViewModel? GetThumbnail(int id);
    public Task<IEnumerable<VehicleThumbnailViewModel>> GetAllThumbnailAsync();
    public IEnumerable<VehicleThumbnailViewModel> GetAllThumbnail();
    public Task SaveDataAsync(VehicleViewModel viewModel);
    public Task<VehicleViewModel?> GetViewModelByIdAsync(int id, bool addSelectList = true);
    public void UpdateData(VehicleViewModel viewModel);
    public Task UpdateDataAsync(VehicleViewModel viewModel);
    public Task RemoveDataAsync(Vehicle entity);
    public Task RemoveDataAsync(int id);
    public void RemoveData(Vehicle entity);
    public Task<bool> ExistsAsync(int id);
    public Task<VehicleViewModel> EmptyViewModelAsync();
}

public class VehiclesRepository(ApplicationDbContext context, IYearRepository yearRepository, IBrandRepository brandRepository, IModelRepository modelRepository, IFinitionRepository finitionRepository) : Repository<Vehicle>(context), IVehiclesRepository
{
    public async Task<IEnumerable<VehicleThumbnailViewModel>> GetAllThumbnailAsync()
    {
        return await Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear).Select(s => s.ConvertToThumbnailViewModel()).ToArrayAsync();
    }
    
    public IEnumerable<VehicleThumbnailViewModel> GetAllThumbnail()
    {
        return Context.Vehicles
            .Include(v => v.Brand)
            .Include(v => v.Finition)
            .Include(v => v.Model)
            .Include(v => v.VehicleYear)
            .Select(s => s.ConvertToThumbnailViewModel()).ToArray();
    }
    
    public async Task<VehicleThumbnailViewModel?> GetThumbnailAsync(int id)
    {
        var model = await GetByIdAsync(id);

        var viewModel = model?.ConvertToThumbnailViewModel();
        return viewModel;
    }

    public VehicleThumbnailViewModel? GetThumbnail(int id)
    {
        var model = GetById(id);
        var viewModel = model?.ConvertToThumbnailViewModel();
        return viewModel;
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
        viewModel.Id = model.Id;
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
    
    public async Task RemoveDataAsync(Vehicle entity)
    {
        if (entity.Id > 0)
        {
            Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
    
    public async Task RemoveDataAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
    
    public void RemoveData(Vehicle entity)
    {
        if (entity.Id > 0)
        {
            Remove(entity);
            Context.SaveChanges();
        }
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await Context.Vehicles.AnyAsync(v => v.Id == id);
    }
}