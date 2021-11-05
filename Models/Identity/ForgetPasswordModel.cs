using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class ForgetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}