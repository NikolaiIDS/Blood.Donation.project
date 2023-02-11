namespace BBDS.Management.Models
{
    public class FilteringViewModel
    {
        public string CityName { get; set; } = null!;

        public Guid CityId { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public int BloodId { get; set; }
        public int PeopleToView { get; set; }
    }
}
