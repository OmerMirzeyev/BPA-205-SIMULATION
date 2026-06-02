using Sim4.Models.Base;

namespace Sim4.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
