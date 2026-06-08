using Sim8.Models.Base;

namespace Sim8.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
