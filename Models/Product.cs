using Simulation_2.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation_2.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile ImageFile {  get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
