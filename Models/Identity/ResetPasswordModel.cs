using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords didn't match")]
        [Display(Name = "Repeat password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}