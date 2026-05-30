using HomeSim3.Models.Base;

namespace HomeSim3.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string FullAdress { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string ImageUrl { get; set; } 
    }
}
