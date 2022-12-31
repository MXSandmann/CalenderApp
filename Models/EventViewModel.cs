using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ZeroTask.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Place { get; set; } = null!;
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [BindProperty, DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
