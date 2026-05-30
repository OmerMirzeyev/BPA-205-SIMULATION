using System.ComponentModel.DataAnnotations;

namespace HomeSim3.ViewModels.Account
{
    public record RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        [
            StringLength(30, ErrorMessage = "Username must be max 30 character"),
            MinLength(2, ErrorMessage = "Username must be min 2 character")
        ]
        public string Username { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 character"),
            MinLength(2, ErrorMessage = "Name must be min 2 character")
        ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [
            StringLength(30, ErrorMessage = "Surname must be max 30 character"),
            MinLength(2, ErrorMessage = "Surname must be min 2 character")
        ]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
