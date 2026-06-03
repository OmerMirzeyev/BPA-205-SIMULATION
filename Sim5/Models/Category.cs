using Sim5.Models.Base;

namespace Sim5.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
