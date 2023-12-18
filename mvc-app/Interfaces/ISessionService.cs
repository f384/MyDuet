using mvc_app.Models;

namespace mvc_app.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAll();
        Task<Session> GetByIdAsync(int id);
        Task<Session> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Session>> GetSessionByCity(string city);
        bool Add(Session Session);
        bool Update(Session Session);
        bool Delate(Session Session);
        bool Save();

    }
}
