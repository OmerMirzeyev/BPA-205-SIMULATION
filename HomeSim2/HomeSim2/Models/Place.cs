using HomeSim2.Models.Base;

namespace HomeSim2.Models
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string ImageURL { get; set; }
    }
}
