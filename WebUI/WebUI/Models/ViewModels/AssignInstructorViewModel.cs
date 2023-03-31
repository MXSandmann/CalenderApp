using Microsoft.Build.Framework;
using WebUI.Models.Dtos;

namespace WebUI.Models.ViewModels
{
    public class AssignInstructorViewModel
    {
        public List<GetInstructorDto> Insructors { get; set; } = null!;
        [Required]
        public string AssignedInstructor { get; set; } = string.Empty;
    }
}
