using mvc_app.Models;

namespace mvc_app.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Session>> GetAllUserSessions();
        Task<List<Studio>> GetAllUserStudios();
    }
}
