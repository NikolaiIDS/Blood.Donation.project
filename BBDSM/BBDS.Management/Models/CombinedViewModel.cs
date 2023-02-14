using BBDS.Management.Data;

namespace BBDS.Management.Models
{
    public class CombinedViewModel
    {
        public List<UsersAcceptedRequests> UsersAcceptedRequests { get; set; }
        public List<RequestViewModel> Requests { get; set; }
        public List<CityViewModel> Cities { get; set; }
    }
}
