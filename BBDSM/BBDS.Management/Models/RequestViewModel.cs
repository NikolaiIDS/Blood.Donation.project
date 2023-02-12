namespace BBDS.Management.Models
{
    public class RequestViewModel
    {
        public Guid Id { get; set; }
        public string CityName { get; set; } = null!;

        public Guid CityId { get; set; }
        public List<CityViewModel> Cities { get; set; }
        public int BloodId { get; set; }
        public List<BloodTypeViewModel> BloodTypes { get; set; }
        public string BloodTypeName { get; set; }
        public int PeopleToView { get; set; }
    }
}
