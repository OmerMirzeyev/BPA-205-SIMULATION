using Sim3.Models.Base;

namespace Sim3.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Crypto> Crypto { get; set; }
    }
}
