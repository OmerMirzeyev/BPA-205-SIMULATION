using System.ComponentModel.DataAnnotations;

namespace Sim8.Areas.Admin.ViewModels.Member
{
    public record CreateMemberVM
    {
        [Required(ErrorMessage = "Name is valid")]
        [MaxLength(100, ErrorMessage = "Name is max 100"),
           MinLength(2, ErrorMessage = "Name is min 2")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is valid")]
        [MaxLength(100, ErrorMessage = "Description is max 100"),
            MinLength(2, ErrorMessage = "Description is min 2")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Surname is valid")]
        [MaxLength(100, ErrorMessage = "Surname is max 100"),
            MinLength(2, ErrorMessage = "Surname is min 2")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "PositionId is valid")]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "ImageFile is valid")]
        public IFormFile ImageFile { get; set; }
    }
}
