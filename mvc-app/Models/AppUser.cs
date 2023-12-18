using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_app.Models
{
    public class AppUser : IdentityUser
    {
        public string? Genre { get; set; }
        public string? Instrument {  get; set; }
        public int? AddressId { get; set; } 
        public Address? Address { get; set; }
        public ICollection<Studio> Studios { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
