using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models.ViewModels
{
    public class AssignInstructorViewModel
    {        
        public Guid InstructorId { get; set; } = Guid.Empty;
        public SelectList Instructors { get; set; } = null!;
    }
}
