using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [Display(Name = "Old password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string Password1 { get; set; }

        [Required]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("Password1", ErrorMessage = "Passwords didn't match")]
        public string Password2 { get; set; }
    }
}