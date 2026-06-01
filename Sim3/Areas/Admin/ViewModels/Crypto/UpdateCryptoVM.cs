using System.ComponentModel.DataAnnotations;

namespace Sim3.Areas.Admin.ViewModels.Crypto
{
    public record UpdateCryptoVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is valid")]
        [
            MaxLength(200, ErrorMessage = "Name is range 2 is 200 characters"),
            MinLength(2, ErrorMessage = "Name is range 2 is 200 characters")
            ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is valid")]

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is valid")]
        [
            MaxLength(200, ErrorMessage = "Description is range 2 is 200 characters"),
            MinLength(2, ErrorMessage = "Description is range 2 is 200 characters")
            ]
        public string Description { get; set; }

        [Required(ErrorMessage = "CategoryId is valid")]

        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = " ImageFile is required")]
        public IFormFile ImageFile { get; set; }
    }
}
