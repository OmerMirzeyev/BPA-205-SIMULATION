using System.ComponentModel.DataAnnotations;

namespace Sim4.Areas.Admin.ViewModels.Member
{
    public record CreateMemberVM
    {
        [Required(ErrorMessage = "Name is valid")]
        [
            MaxLength(30, ErrorMessage = "Name is max 30"),
            MinLength(2, ErrorMessage = "Name is min 2")
            ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is valid")]
        [
            MaxLength(30, ErrorMessage = "Surname is max 30"),
            MinLength(2, ErrorMessage = "Surname is min 2")
            ]
        public string Surname  { get; set; }
        [Required(ErrorMessage = "Description is valid")]
        [
            MaxLength(30, ErrorMessage = "Description is max 30"),
            MinLength(2, ErrorMessage = "Description is min 2")
            ]
        public string Description { get; set; }

        public int PositionId { get; set; }

        [Required(ErrorMessage = "ImageUrl is valid")]
        public string? ImageUrl { get; set; }
        public IFormFile ImageFile {  get; set; }
    }
}
