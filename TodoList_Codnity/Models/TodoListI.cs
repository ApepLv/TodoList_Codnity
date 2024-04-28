using System.ComponentModel.DataAnnotations;

namespace TodoList_Codnity.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}