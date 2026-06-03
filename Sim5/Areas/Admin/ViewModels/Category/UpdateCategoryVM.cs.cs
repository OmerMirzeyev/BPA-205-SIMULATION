using System.ComponentModel.DataAnnotations;

namespace Sim5.Areas.Admin.ViewModels.Category
{
    public record UpdateCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is valid")]
        [
           MaxLength(30, ErrorMessage = "Name is max 30character"),
           MinLength(2, ErrorMessage = "Name is min 2character")
           ]
        public string Name { get; set; }
    }
}
