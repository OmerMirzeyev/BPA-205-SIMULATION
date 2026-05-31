using System.ComponentModel.DataAnnotations;

namespace HomeSim4.Areas.Admin.ViewModels.Employee
{
    public record UpdateEmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 50 characters")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 50 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int PositionId { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "ImageFile is required")]
        public IFormFile? ImageFile { get; set; }
    }
}
