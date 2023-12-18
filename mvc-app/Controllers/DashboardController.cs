using Microsoft.AspNetCore.Mvc;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.ViewModels;

namespace mvc_app.Controllers
{
    public class DashboardController : Controller
    {
        
        private IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService) 
        {
            _dashboardService = dashboardService;
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
    }
}
