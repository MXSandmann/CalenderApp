using ApplicationCore.Models;
using ApplicationCore.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models
{
    public class UserEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Place { get; set; } = null!;
        [BindProperty]
        public DateTime StartDateTime { get; set; }
        [BindProperty]
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public Recurrency Recurrency { get; set; }

        public UserEvent ToUserEvent()
        {
            return new UserEvent
            {
                Id = this.Id,
                Name = this.Name,
                Category = this.Category,
                Place = this.Place,                
                StartDateTime = this.StartDateTime,
                EndDateTime = this.EndDateTime,
                Description = this.Description,
                AdditionalInfo = this.AdditionalInfo,
                ImageUrl = this.ImageUrl,
                Recurrency = this.Recurrency
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
                StartDateTime = userEvent.StartDateTime,
                EndDateTime = userEvent.EndDateTime,
                Description = userEvent.Description,
                AdditionalInfo = userEvent.AdditionalInfo,
                ImageUrl = userEvent.ImageUrl,
                Recurrency = userEvent.Recurrency
            };
        }
    }
}
