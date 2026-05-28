using System.ComponentModel.DataAnnotations;

namespace HomeSim2.Areas.Admin.ViewModels.Place
{
    public record CreatePlaceVM
    {
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 2 ch")
        ]
        public string Name { get; set; }

        [Required(ErrorMessage = "FullAddress is required")]
        [
            StringLength(50, ErrorMessage = "FullAddress must be max 30 ch"),
            MinLength(2, ErrorMessage = "FullAddress must be min 2 ch")
        ]
        public string FullAddress { get; set; }


        [Required(ErrorMessage = "CityId is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "FormFile is required")]
        public IFormFile ImageFile { get; set; }
    }
}
