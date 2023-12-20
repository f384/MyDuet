using mvc_app.Models;

namespace mvc_app.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Session>> GetAllUserSessions();
        Task<List<Studio>> GetAllUserStudios();
        Task<AppUser> GetByIdNoTracking(string id);
        Task<AppUser> GetUserById(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
