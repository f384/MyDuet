using mvc_app.Models;

namespace mvc_app.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Studio> Studios { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    } 
}
