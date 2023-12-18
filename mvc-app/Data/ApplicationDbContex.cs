using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvc_app.Models;

namespace mvc_app.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Address> Addresses{ get; set; }  
    }
}
