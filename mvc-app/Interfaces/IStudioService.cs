using mvc_app.Models;

namespace mvc_app.Interfaces
{
    public interface IStudioService
    {
        Task<IEnumerable<Studio>> GetAll();
        Task<Studio> GetByIdAsync(int id);
        Task<Studio> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Studio>> GetStudioByCity(string city);
        bool Add(Studio studio);
        bool Update(Studio studio);
        bool Delate(Studio studio);
        bool Save();

    }
}
