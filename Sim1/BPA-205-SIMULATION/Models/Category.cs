using Simulation_2.Models.Base;

namespace Simulation_2.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
