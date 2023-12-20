using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_app.Models
{
    public class AppUser : IdentityUser
    {
        public string? Genre { get; set; }
        public string? Instrument {  get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; } 
        public Address? Address { get; set; }
        public ICollection<Studio> Studios { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
