using Simulation_2.Models;

namespace Simulation_2.Areas.Admin.ViewModels.Product
{
    public record CreateProductVM
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        //public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
