using Microsoft.AspNetCore.Mvc;
using mvc_app.Helpers;
using mvc_app.Interfaces;
using mvc_app.Models;
using mvc_app.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace mvc_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudioService _studioService;

        public HomeController(ILogger<HomeController> logger, IStudioService studioService)
        {
            _logger = logger;
            _studioService = studioService;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=78d7e96ac374d3";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if(homeViewModel.City != null)
                {
                    homeViewModel.Studios = await _studioService.GetStudioByCity(homeViewModel.City);  
                }
                else
                {
                    homeViewModel.Studios = null;
                }
            }
            catch (Exception ex)
            {
                homeViewModel.Studios = null;
            }
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}