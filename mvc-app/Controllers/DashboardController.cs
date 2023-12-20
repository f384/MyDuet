using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;
using mvc_app.ViewModels;

namespace mvc_app.Controllers
{
    public class DashboardController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IDashboardService _dashboardService;
        private IPhotoService _photoService;

        public DashboardController(IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor, IPhotoService photoService) 
        {
            _httpContextAccessor = httpContextAccessor;
            _dashboardService = dashboardService;
            _photoService = photoService;
        }
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Genre = editVM.Genre;
            user.Instrument = editVM.Instrument;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.State = editVM.State;
        }

        public async Task<IActionResult> Index()
        {
            var userSessions = await _dashboardService.GetAllUserSessions();
            var userStudios = await _dashboardService.GetAllUserStudios();
            var userViewModel = new DashboardViewModel()
            {
                Sessions = userSessions,
                Studios = userStudios
            };
            return View(userViewModel);
        }
        
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardService.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Genre = user.Genre,
                Instrument = user.Instrument,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }
            var user = await _dashboardService.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardService.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardService.Update(user);

                return RedirectToAction("Index");   
            }
           
        }
       
    }
}
