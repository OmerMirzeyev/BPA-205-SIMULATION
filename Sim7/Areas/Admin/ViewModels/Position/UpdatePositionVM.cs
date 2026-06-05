using System.ComponentModel.DataAnnotations;

namespace Sim7.Areas.Admin.ViewModels.Position
{
    public record UpdatePositionVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is valid")]
        [MaxLength(100, ErrorMessage = "Name is max 100"),
            MinLength(2, ErrorMessage = "Name is min 2")]
        public string Name { get; set; }
    }
}
