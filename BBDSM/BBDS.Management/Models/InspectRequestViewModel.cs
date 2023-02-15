using BBDS.Management.Data;

namespace BBDS.Management.Models
{
    public class InspectRequestViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public Request Request { get; set; }
    }
}
