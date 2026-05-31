using HomeSim4.Models.Base;

namespace HomeSim4.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
