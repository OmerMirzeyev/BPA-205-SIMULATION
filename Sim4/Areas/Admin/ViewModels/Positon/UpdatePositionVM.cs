using System.ComponentModel.DataAnnotations;

namespace Sim4.Areas.Admin.ViewModels.Positon
{
    public record UpdatePositionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is valid")]
        [
           MaxLength(30, ErrorMessage = "Name is max 30"),
           MinLength(2, ErrorMessage = "Name is min 2")
           ]
        public string Name { get; set; }
    }
}
