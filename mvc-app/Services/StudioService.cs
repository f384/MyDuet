using Microsoft.EntityFrameworkCore;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;

namespace mvc_app.Services
{
    public class StudioService : IStudioService
    {
        private readonly ApplicationDbContext _context;

        public StudioService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Studio studio)
        {

            _context.Add(studio);
            return Save();
        }

        public bool Delate(Studio studio)
        {
            _context.Remove(studio);
            return Save();
        }

        public async Task<IEnumerable<Studio>> GetAll()
        {
            return await _context.Studios.ToListAsync();
        }

        public async Task<Studio?> GetByIdAsync(int id)
        {
            return await _context.Studios.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<Studio> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Studios.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Studio>> GetStudioByCity(string city)
        {
            return await _context.Studios.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Studio studio)
        {
            _context.Update(studio);
            return Save();
        }
    }
}
