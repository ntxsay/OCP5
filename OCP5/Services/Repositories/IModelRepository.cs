using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IModelRepository : IRepository<Model>
{
    /// <summary>
    /// Récupère tous les modèles d'une marque spécifique.
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<IEnumerable<Model>> GetAllAsync(int brandId);
    
    /// <summary>
    /// Génère une liste de sélection de tous les modèles.
    /// </summary>
    /// <returns></returns>
    Task<SelectList> GetSelectListAsync();
    
    /// <summary>
    /// Génère une liste de sélection des modèles d'une marque spécifique.
    /// </summary>
    /// <param name="brandId"></param>
    /// <returns></returns>
    Task<SelectList> GetSelectListAsync(int brandId);
}

public class ModelRepository(ApplicationDbContext context) : Repository<Model>(context), IModelRepository
{
    public virtual async Task<IEnumerable<Model>> GetAllAsync(int brandId)
    {
        return await Context.Models.Where(m => m.IdBrand == brandId).ToArrayAsync();
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