using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Data;

namespace TodoList.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Short description")]
        [MaxLength(128)]
        [Required]
        public string ShortDescription { get; set; }
        
        [Display(Name = "Detailed description")]
        [MaxLength(1024)]
        public string DetailedDescription { get; set; }

        [Display(Name = "Creation date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Completion date")]
        [DataType(DataType.DateTime)]
        public DateTime? CompletionDate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public bool IsDone { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get
            {
                if (IsDone)
                    return false;
                if (CompletionDate is null)
                    return true;
                
                return DateTime.Now < CompletionDate;
            }
        }
        
        public TaskModel()
        {
            CreationDate = DateTime.Now;
            CompletionDate = null;
            IsDone = false;
        }

        public string GetTaskStatus()
        {
            if (IsActive)
            {
                if (!IsDone) return "In progress";
                return "???";
            }
            else
            {
                if (IsDone) return "Done";
                else return "Expired";
            }
        }
    }
}