using Sim5.Models.Base;

namespace Sim5.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
