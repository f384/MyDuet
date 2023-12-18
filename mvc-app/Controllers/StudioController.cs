using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;
using mvc_app.Services;
using mvc_app.ViewModels;

namespace mvc_app.Controllers
{
    public class StudioController : Controller
   {
        private readonly IStudioService _studioService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudioController(IStudioService studioService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _studioService = studioService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Studio> studios = await _studioService.GetAll();
            return View(studios);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Studio studio = await _studioService.GetByIdAsync(id);
            return View(studio);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createStudioViewModel = new CreateStudioViewModel { AppUserId = curUserId };
            return View(createStudioViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudioViewModel studioVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(studioVM.Image);

                var studio = new Studio
                {
                    Title = studioVM.Title,
                    Description = studioVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = studioVM.AppUserId,
                    Address = new Address
                    {
                        City = studioVM.Address.City,
                        Street = studioVM.Address.Street,
                        State = studioVM.Address.State
                    }

                };
            _studioService.Add(studio);
            return RedirectToAction("Index");
            } 
           else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(studioVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _studioService.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task <IActionResult> DeleteStudio(int id)
        {
            var studioDetails = await _studioService.GetByIdAsync(id);
            if (studioDetails == null) return View("Error");

            _studioService.Delate(studioDetails);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var studio = await _studioService.GetByIdAsync(id);
            if (studio == null) return View("Error");
            var studioVM = new EditStudioViewModel
            {
                Title = studio.Title,
                Description = studio.Description,
                AddressId = studio.AddressId,
                Address = studio.Address,
                URL = studio.Image,
                StudioCategory = studio.StudioCategory

            };
                return View(studioVM);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStudioViewModel StudioVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Studio");
                return View("Edit", StudioVM);
            }

            var userStudio = await _studioService.GetByIdAsyncNoTracking(id);

            if (userStudio == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(StudioVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(StudioVM);
            }

            if (!string.IsNullOrEmpty(userStudio.Image))
            {
                _ = _photoService.DeletePhotoAsync(userStudio.Image);
            }

            var Studio = new Studio
            {
                Id = id,
                Title = StudioVM.Title,
                Description = StudioVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = StudioVM.AddressId,
                Address = StudioVM.Address,
            };

            _studioService.Update(Studio);

            return RedirectToAction("Index");
        }
    }
}
