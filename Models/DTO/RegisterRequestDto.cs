using System.ComponentModel.DataAnnotations;

namespace KindHeartCharity.Models.DTO
{
    public class RegisterRequestDto
    {

        [Required(ErrorMessage = "Field can't be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Field can't be empty")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Field can't be empty")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password does not match")]
        public string ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
