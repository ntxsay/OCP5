using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Extensions;
using OCP5.Models.Entities;
using OCP5.Models.ViewModels;
using OCP5.Services;
using OCP5.Services.Repositories;

namespace OCP5.Controllers
{
    public class VehiclesController(IVehiclesRepository vehiculeRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var models = await vehiculeRepository.GetAllThumbnailAsync();
            return View(models);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = await vehiculeRepository.GetThumbnailAsync(id.Value);
            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = await vehiculeRepository.EmptyViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await vehiculeRepository.SaveDataAsync(viewModel);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return View("Published");
            }

            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await vehiculeRepository.GetViewModelByIdAsync(id.Value);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!await vehiculeRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                
                try
                {
                    await vehiculeRepository.UpdateDataAsync(viewModel);
                }
                catch (Exception)
                {
                   return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var model = await vehiculeRepository.GetByIdAsync(id.Value);
            if (model == null)
                return NotFound();
            
            var fullName = $"{model.VehicleYear.Year} {model.Brand.Name} {model.Model.Name} {model.Finition.Name}";
            
            await vehiculeRepository.RemoveDataAsync(model);
            
            return View("DeletedConfirmation", fullName);
        }
    }
}
