using mvc_app.Models;

namespace mvc_app.ViewModels
{
    public class DashboardViewModel
    {
        public List<Session> Sessions { get; set; }
        public List<Studio> Studios { get; set; }
    }
}
