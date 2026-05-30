using HomeSim3.Models.Base;

namespace HomeSim3.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
