using mvc_app.Data.Enum;
using mvc_app.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace mvc_app.ViewModels
{
    public class CreateStudioViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public  IFormFile Image { get; set; }
        public StudioCategory StudioCategory { get; set; }
        public string AppUserId { get; set; }

    }
}
