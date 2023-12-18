using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;
using mvc_app.Services;
using mvc_app.ViewModels;

namespace mvc_app.Controllers
{
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ISessionService _sessionService;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;
        public SessionController(ISessionService sessionService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _sessionService = sessionService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Session> Sessions =  await _sessionService.GetAll();
            return View(Sessions);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Session session = await _sessionService.GetByIdAsync(id);
            return View(session);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createSessionViewModel = new CreateSessionViewModel { AppUserId = curUserId };
            return View(createSessionViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel sessionVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(sessionVM.Image);

                var session = new Session
                {
                    Title = sessionVM.Title,
                    Description = sessionVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = sessionVM.AppUserId,
                    Address = new Address
                    {
                        City = sessionVM.Address.City,
                        Street = sessionVM.Address.Street,
                        State = sessionVM.Address.State,
                    }

                };
                _sessionService.Add(session);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(sessionVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sessionDetails = await _sessionService.GetByIdAsync(id);
            if (sessionDetails == null) return View("Error");
            return View(sessionDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var sessionDetails = await _sessionService.GetByIdAsync(id);
            if (sessionDetails == null) return View("Error");

            _sessionService.Delate(sessionDetails);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return View("Error");
            }
            var sessionVM = new EditSessionViewModel
            {
                Title = session.Title,
                Description = session.Description,
                AddressId = session.AddressId,
                Address = session.Address,
                URL = session.Image,
                SessionCategory = session.SessionCategory
            };
            return View(sessionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSessionViewModel SessionVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Session");
                return View("Edit", SessionVM);
            }

            var userSession = await _sessionService.GetByIdAsyncNoTracking(id);

            if (userSession == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(SessionVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(SessionVM);
            }

            if (!string.IsNullOrEmpty(userSession.Image))
            {
                _ = _photoService.DeletePhotoAsync(userSession.Image);
            }

            var Session = new Session
            {
                Id = id,
                Title = SessionVM.Title,
                Description = SessionVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = SessionVM.AddressId,
                Address = SessionVM.Address,
            };

            _sessionService.Update(Session);

            return RedirectToAction("Index");
        }
    }
}
