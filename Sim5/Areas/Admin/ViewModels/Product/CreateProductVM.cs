namespace Sim5.Areas.Admin.ViewModels.Product
{
    public record CreateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
