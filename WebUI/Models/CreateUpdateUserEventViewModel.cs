﻿using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{

    public class CreateUpdateUserEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Place { get; set; } = null!;
        [BindProperty, DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [BindProperty, DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime? LastDate { get; set; }
        public string Description { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public YesNo HasRecurrency { get; set; }
        public Recurrency? Recurrency { get; set; }
        public int? Gap { get; set; }
        public int? MaximumOccurrencies { get; set; }
        //public DayOfTheWeek? DayOfWeek { get; set; }
        public bool OnMonday { get; set; }
        public bool OnTuesday { get; set; }
        public bool OnWednesday { get; set; }
        public bool OnThursday { get; set; }
        public bool OnFriday { get; set; }
        public bool OnSaturday { get; set; }
        public bool OnSunday { get; set; }
        public WeekOfTheMonth? WeekOfMonth { get; set; }
        public MonthOfTheYear? MonthOfYear { get; set; }

        public UserEvent ToUserEvent()
        {
            return new UserEvent
            {
                Id = this.Id,
                Name = this.Name,
                Category = this.Category,
                Place = this.Place,                
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Date = this.Date,
                LastDate = this.LastDate ?? this.Date,
                Description = this.Description,
                AdditionalInfo = this.AdditionalInfo,
                ImageUrl = this.ImageUrl,
                HasRecurrency = this.HasRecurrency
            };
        }

        public RecurrencyRule ToRecurrencyRule()
        {
            return new RecurrencyRule
            {
                Recurrency = this.Recurrency ?? default,
                Gap = this.Gap ?? default,
                MaximumOccurrencies = this.MaximumOccurrencies ?? default,
                //DayOfWeek = this.DayOfWeek ?? default,
                WeekOfMonth = this.WeekOfMonth ?? default,
                MonthOfYear = this.MonthOfYear ?? default,
                OnMonday = this.OnMonday,
                OnTuesday = this.OnTuesday,
                OnWednesday = this.OnWednesday,
                OnThursday = this.OnThursday,
                OnFriday = this.OnFriday,
                OnSaturday = this.OnSaturday,
                OnSunday = this.OnSunday
            };
        }

        public static CreateUpdateUserEventViewModel ToUserEventViewModel(UserEvent userEvent)
        {
            return new CreateUpdateUserEventViewModel
            {
                Id = userEvent.Id,
                Name = userEvent.Name,
                Category = userEvent.Category,
                Place = userEvent.Place,
                StartTime = userEvent.StartTime,
                EndTime = userEvent.EndTime,
                Date = userEvent.Date,
                LastDate = userEvent.LastDate,
                Description = userEvent.Description,
                AdditionalInfo = userEvent.AdditionalInfo,
                ImageUrl = userEvent.ImageUrl,
                HasRecurrency = userEvent.HasRecurrency,
                Recurrency = userEvent.RecurrencyRule?.Recurrency ?? default,
                Gap = userEvent.RecurrencyRule?.Gap ?? default,
                MaximumOccurrencies = userEvent.RecurrencyRule?.MaximumOccurrencies ?? default,
                //DayOfWeek = userEvent.RecurrencyRule?.DayOfWeek ?? default,
                WeekOfMonth = userEvent.RecurrencyRule?.WeekOfMonth ?? default,
                MonthOfYear = userEvent.RecurrencyRule?.MonthOfYear ?? default,
                OnMonday = userEvent.RecurrencyRule?.OnMonday ?? false,
                OnTuesday = userEvent.RecurrencyRule?.OnTuesday ?? false,
                OnWednesday = userEvent.RecurrencyRule?.OnWednesday ?? false,
                OnThursday = userEvent.RecurrencyRule?.OnThursday ?? false,
                OnFriday = userEvent.RecurrencyRule?.OnFriday ?? false,
                OnSaturday = userEvent.RecurrencyRule?.OnSaturday ?? false,
                OnSunday = userEvent.RecurrencyRule?.OnSunday ?? false
            };
        }
    }    
}
