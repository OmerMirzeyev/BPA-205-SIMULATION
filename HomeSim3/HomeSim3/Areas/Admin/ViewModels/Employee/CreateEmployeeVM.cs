using System.ComponentModel.DataAnnotations;

namespace HomeSim3.Areas.Admin.ViewModels.Employee
{
    public record CreateEmployeeVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "FullAdress is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "FullAdress must be between 2 and 50 characters")]
        public string FullAdress { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "ImageFile is required")]
        public IFormFile? ImageFile { get; set; }
    }
}
