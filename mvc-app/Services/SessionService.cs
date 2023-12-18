using Microsoft.EntityFrameworkCore;
using mvc_app.Data;
using mvc_app.Interfaces;
using mvc_app.Models;

namespace mvc_app.Services
{
    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _context;
        public SessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Session session)
        {

            _context.Add(session);
            return Save();
        }

        public bool Delate(Session session)
        {
            _context.Remove(session);
            return Save();
        }

        public async Task<IEnumerable<Session>> GetAll()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Session> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Sessions.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Session>> GetSessionByCity(string city)
        {
            return await _context.Sessions.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Session session)
        {
            _context.Update(session);
            return Save();
        }
    }
}
 