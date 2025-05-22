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
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var models = await vehiculeRepository.GetAllAsync();
            return View(models);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var vehicle = await vehiculeRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = await vehiculeRepository.EmptyViewModelAsync();
            return View(viewModel);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await vehiculeRepository.SaveDataAsync(viewModel);
                
                return RedirectToAction(nameof(Index));
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

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    vehiculeRepository.UpdateDataAsync(viewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                   return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await vehiculeRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await vehiculeRepository.GetByIdAsync(id);
            if (vehicle != null)
            {
                vehiculeRepository.Remove(vehicle);
            }

            await vehiculeRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
