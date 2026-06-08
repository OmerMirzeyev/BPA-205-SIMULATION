using Sim8.Models.Base;

namespace Sim8.Models
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Surname { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string ImageUrl { get; set; }
    }
}
