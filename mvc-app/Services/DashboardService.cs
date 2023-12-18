using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;

namespace mvc_app.Services
{
    public class DashboardService : IDashboardService
    {
        private ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public DashboardService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }   
        public async Task<List<Session>> GetAllUserSessions()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userSessions = _context.Sessions.Where(r => r.AppUser.Id == curUser);
            return userSessions.ToList(); //to check async        
        }

           public async Task<List<Studio>> GetAllUserStudios()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userStudios = _context.Studios.Where(r => r.AppUser.Id == curUser);
            return userStudios.ToList(); //to check 
        }
    }
}
