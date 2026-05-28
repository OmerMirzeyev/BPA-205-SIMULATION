using System.ComponentModel.DataAnnotations;

namespace HomeSim2.Areas.Admin.ViewModels.City
{
    public record CreateCityVM
    {
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 2 ch")
        ]
        public string Name { get; set; }
    }
}
