using HomeSim.Models.Base;

namespace HomeSim.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Position? Position { get; set; }
        public int PositionId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
