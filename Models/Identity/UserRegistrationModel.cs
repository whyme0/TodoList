using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password1 { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password1", ErrorMessage = "Passwords didn't match")]
        [Display(Name = "Repeat password")]
        public string Password2 { get; set; }
    }
}