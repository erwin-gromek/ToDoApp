using System.ComponentModel;

namespace ToDoAppWeb.Models
{
    public class TaskToDo
    {
        public int Id { get; set; }
        [DisplayName("Priority")]
        public int Priority_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        [DisplayName("Creation/Edit Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
