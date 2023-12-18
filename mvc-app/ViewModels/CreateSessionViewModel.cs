using mvc_app.Data.Enum;
using mvc_app.Models;

namespace mvc_app.ViewModels
{
    public class CreateSessionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public SessionCategory SessionCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
