using System.ComponentModel.DataAnnotations;

namespace Sim3.Areas.Admin.ViewModels.Category
{
    public record CreateCategoryVM
    {
        [Required(ErrorMessage = "Name is valid")]
        [
           MaxLength(200, ErrorMessage = "Name is range 2 is 200 characters"),
           MinLength(2, ErrorMessage = "Name is range 2 is 200 characters")
           ]
        public string Name { get; set; }
    }
}
