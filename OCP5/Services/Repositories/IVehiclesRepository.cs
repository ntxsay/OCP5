using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Extensions;
using OCP5.Models.Entities;
using OCP5.Models.ViewModels;

namespace OCP5.Services.Repositories;

public interface IVehiclesRepository : IRepository<Vehicle>
{
    /// <summary>
    /// Retourne le nom de l'image d'un véhicule par son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<string?> GetImageFileNameAsync(int id);
    
    /// <summary>
    /// Retourne le nom de l'image d'un véhicule par son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string? GetImageFileName(int id);
    
    /// <summary>
    /// Retourne un modèle de vue pour l'affichage d'un véhicule par son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<VehicleThumbnailViewModel?> GetThumbnailAsync(int id);
    
    /// <summary>
    /// Retourne un modèle de vue pour l'affichage d'un véhicule par son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public VehicleThumbnailViewModel? GetThumbnail(int id);
    
    /// <summary>
    /// Retourne une énumération de tous les véhicules sous forme de modèles de vue pour l'affichage des vignettes de manière asynchrone.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<VehicleThumbnailViewModel>> GetAllThumbnailAsync();
    
    /// <summary>
    /// Retourne une énumération de tous les véhicules sous forme de modèles de vue pour l'affichage des vignettes.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<VehicleThumbnailViewModel> GetAllThumbnail();
    
    /// <summary>
    /// Enregistre les données d'un véhicule à partir d'un modèle de vue de formulaire et de manière asynchrone.
    /// </summary>
    /// <param name="viewModel">Modèle de vue de formulaire</param>
    /// <returns></returns>
    public Task SaveDataAsync(VehicleViewModel viewModel);
    
    /// <summary>
    /// Retourne un modèle de véhicule de formulaire par son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<VehicleViewModel?> GetViewModelByIdAsync(int id);
    
    /// <summary>
    /// Met à jour les données d'un véhicule à partir d'un modèle de vue de formulaire.
    /// </summary>
    /// <param name="viewModel"></param>
    public void UpdateData(VehicleViewModel viewModel);
    
    /// <summary>
    /// Met à jour les données d'un véhicule à partir d'un modèle de vue de formulaire de manière asynchrone.
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public Task UpdateDataAsync(VehicleViewModel viewModel);
    
    /// <summary>
    /// Supprime un véhicule à partir de son entité de manière asynchrone.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task RemoveDataAsync(Vehicle entity);
    
    /// <summary>
    /// Supprime un véhicule à partir de son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task RemoveDataAsync(int id);
    
    /// <summary>
    /// Supprime un véhicule à partir de son entité.
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveData(Vehicle entity);
    
    /// <summary>
    /// Vérifie si un véhicule existe par son identifiant de manière asynchrone.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(int id);
    
    /// <summary>
    /// Retourne un dictionnaire des modèles d'une marque spécifique de manière asynchrone.
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    public Task<Dictionary<int, string>> GetModelsAsync(int brandId);
    
    /// <summary>
    /// Instancie un nouveau modèle de vue de véhicule
    /// </summary>
    /// <returns></returns>
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
    
    public async Task<string?> GetImageFileNameAsync(int id)
    {
        var model = await Context.Vehicles.Where(v => v.Id == id)
            .Select(v => v.ImageFileName)
            .SingleOrDefaultAsync();
        return model;
    }
    
    public string? GetImageFileName(int id)
    {
        var model = Context.Vehicles.Where(v => v.Id == id)
            .Select(v => v.ImageFileName)
            .SingleOrDefault();
        return model;
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
    
    public async Task<VehicleViewModel?> GetViewModelByIdAsync(int id)
    {
        var model = await GetByIdAsync(id);
        if (model == null)
            return null;
        
        var viewModel = model.ConvertToViewModel();
        viewModel.Brands = await brandRepository.GetSelectListAsync();
        viewModel.Models = viewModel.Brands.Any()
            ? await modelRepository.GetSelectListAsync(Convert.ToInt32(viewModel.Brands.First().Value))
            : new SelectList(Enumerable.Empty<SelectListItem>());

        viewModel.VehicleYears = await yearRepository.GetSelectListAsync();
        viewModel.Finitions = await finitionRepository.GetSelectListAsync();
        
        return viewModel;
    }
    
    public async Task<VehicleViewModel> EmptyViewModelAsync()
    {
        var brands = await brandRepository.GetSelectListAsync();
        var models = brands.Any()
            ? await modelRepository.GetSelectListAsync(Convert.ToInt32(brands.First().Value))
            : new SelectList(Enumerable.Empty<SelectListItem>());
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

    public async Task<Dictionary<int, string>> GetModelsAsync(int brandId)
    {
        return await brandRepository.GetModelsAsync(brandId);
    }
}