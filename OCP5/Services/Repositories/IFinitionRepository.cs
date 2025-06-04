using Microsoft.AspNetCore.Mvc.Rendering;
using OCP5.Data;
using OCP5.Models.Entities;

namespace OCP5.Services.Repositories;

public interface IFinitionRepository : IRepository<Finition>
{
    /// <summary>
    /// Génère une liste de sélection de toutes les finitions.
    /// </summary>
    /// <returns></returns>
    Task<SelectList> GetSelectListAsync();
}

public class FinitionRepository(ApplicationDbContext context) : Repository<Finition>(context), IFinitionRepository
{
    public async Task<SelectList> GetSelectListAsync()
    {
        return new SelectList(await GetAllAsync(), nameof(Finition.Id), nameof(Finition.Name));
    }
}