using Sim7.Models.Base;

namespace Sim7.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
