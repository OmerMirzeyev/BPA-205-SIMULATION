using HomeSim4.Models.Base;

namespace HomeSim4.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
