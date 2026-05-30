using System.ComponentModel.DataAnnotations;

namespace HomeSim3.Areas.Admin.ViewModels.Position
{
    public record UpdatePositionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 3 ch")
        ]
        public string Name { get; set; }
    }
}
