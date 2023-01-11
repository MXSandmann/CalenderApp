using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZeroTask.BLL.Models;

namespace ZeroTask.PL.Models
{
    public class UserEventViewModel
    {
        public Guid Id { get; set; }
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

        public UserEvent ToUserEvent()
        {
            return new UserEvent
            {
                Id = this.Id,
                Name = this.Name,
                Category = this.Category,
                Place = this.Place,
                Date = this.Date,
                Time = this.Time,
                Description = this.Description,
                AdditionalInfo = this.AdditionalInfo,
                ImageUrl = this.ImageUrl
            };
        }

        public static UserEventViewModel ToUserEventViewModel(UserEvent userEvent)
        {
            return new UserEventViewModel
            {
                Id = userEvent.Id,
                Name = userEvent.Name,
                Category = userEvent.Category,
                Place = userEvent.Place,
                Date = userEvent.Date,
                Time = userEvent.Time,
                Description = userEvent.Description,
                AdditionalInfo = userEvent.AdditionalInfo,
                ImageUrl = userEvent.ImageUrl
            };
        }
    }
}
