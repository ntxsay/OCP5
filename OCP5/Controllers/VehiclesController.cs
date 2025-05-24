using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCP5.Models.ViewModels;
using OCP5.Services;
using OCP5.Services.Repositories;

namespace OCP5.Controllers
{
    public class VehiclesController(IVehiclesRepository vehiculeRepository, IFileUploadService fileUploadService) : Controller
    {
        /// <summary>
        /// Types de fichiers aautorisés pour un fichier à mettre en ligne
        /// </summary>
        private readonly string[] _autorizeContentTypes = ["image/png", "image/jpeg", "image/jpg", "image/webp"];
        
        /// <summary>
        /// Taille autorisée pour un fichier à mettre en ligne : environ 2Mb
        /// </summary>
        private const long MaxSizeAutorized = 2097152;
        
        private const string UploadsFolderName = "Uploads";
        
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
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var viewModel = await vehiculeRepository.EmptyViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.File != null)
                    {
                        var fileName = await fileUploadService.UploadFileAsync(viewModel.File, UploadsFolderName, _autorizeContentTypes, MaxSizeAutorized);
                        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrWhiteSpace(fileName))
                            viewModel.ImageFileName = fileName;
                    }
                    
                    await vehiculeRepository.SaveDataAsync(viewModel);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return View("Published");
            }

            viewModel = await vehiculeRepository.EmptyViewModelAsync();
            return View(viewModel);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!await vehiculeRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                
                try
                {
                    if (viewModel.File != null)
                    {
                        var fileName = await fileUploadService.UploadFileAsync(viewModel.File, UploadsFolderName, _autorizeContentTypes, MaxSizeAutorized);
                        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrWhiteSpace(fileName))
                            viewModel.ImageFileName = fileName;
                        
                        //Supprime l'ancienne image
                        var currentImageFileName = await vehiculeRepository.GetImageFileNameAsync(id);
                        if (!string.IsNullOrEmpty(currentImageFileName) && !string.IsNullOrWhiteSpace(currentImageFileName))
                            fileUploadService.DeleteFile(UploadsFolderName, currentImageFileName);
                    }
                    await vehiculeRepository.UpdateDataAsync(viewModel);
                }
                catch (Exception)
                {
                   return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel = await vehiculeRepository.EmptyViewModelAsync();
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var model = await vehiculeRepository.GetByIdAsync(id.Value);
            if (model == null)
                return NotFound();

            //Supprime l'image
            var currentImageFileName = model.ImageFileName;
            if (!string.IsNullOrEmpty(currentImageFileName) && !string.IsNullOrWhiteSpace(currentImageFileName))
                fileUploadService.DeleteFile(UploadsFolderName, currentImageFileName);
            
            var fullName = $"{model.VehicleYear.Year} {model.Brand.Name} {model.Model.Name} {model.Finition.Name}";
            
            await vehiculeRepository.RemoveDataAsync(model);
            
            return View("DeletedConfirmation", fullName);
        }
        
        [HttpGet]
        [ActionName("ImageByName")]
        public FileResult? GetImageByName(string? fileName)
        {
            var file = fileUploadService.GetFile(UploadsFolderName, fileName);
            return file;
        }
    }
}
