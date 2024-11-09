using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a name for the task.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a description for the task.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter a due date.")]
        [YearFromNow(100, IsPast = true)]
        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Please enter sprint number for task.")]
        public int? SprintNumber { get; set; } = null!;
        [Required(ErrorMessage = "Please enter the point value for the task.")]
        [Range(0 , 250, ErrorMessage ="Please enter a point value between 0 and 250.")]
        public int? PointValue { get; set; } = null!;
        [Required(ErrorMessage = "Please select a status.")]
        public string StatusID { get; set; } = string.Empty;
       
        public Status Status { get; set; } = null!;

        public bool Overdue => StatusID != "done" && DueDate < DateTime.Today;
    }
}
