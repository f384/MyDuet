using Microsoft.AspNetCore.Mvc;
using mvc_app.Interfaces;
using mvc_app.ViewModels;

namespace mvc_app.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Genre = user.Genre,
                    Instrument = user.Instrument,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                result.Add(userViewModel);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userService.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Genre = user.Genre,
                Instrument = user.Instrument
            };
            return View(userDetailViewModel);
        }
    }
}
