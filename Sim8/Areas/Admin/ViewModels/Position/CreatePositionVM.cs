using System.ComponentModel.DataAnnotations;

namespace Sim8.Areas.Admin.ViewModels.Position
{
    public record CreatePositionVM
    {
        [Required(ErrorMessage = "Name is valid")]
        [MaxLength(100, ErrorMessage = "Name is max 100"),
           MinLength(2, ErrorMessage = "Name is min 2")]
        public string Name { get; set; }
    }
}
