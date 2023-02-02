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
        public bool HasRecurrency { get; set; }
        public Recurrency? Recurrency { get; set; }
        public bool OnMonday { get; set; }
        public bool OnTuesday { get; set; }
        public bool OnWednesday { get; set; }
        public bool OnThursday { get; set; }
        public bool OnFriday { get; set; }
        public bool OnSaturday { get; set; }
        public bool OnSunday { get; set; }
        public WeekOfTheMonth? WeekOfMonth { get; set; }
        public bool OnWorkingDays { get; set; }
        public bool OnWeekend { get; set; }
        public EvenOdd EvenOdd { get; set; }

        /// <summary>
        /// Map viewmodel to user events
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Map viewmodel to recurrency rule
        /// </summary>
        /// <returns></returns>
        public RecurrencyRule ToRecurrencyRule()
        {
            var certaindays = (OnMonday || OnWorkingDays) ? CertainDays.Monday : 0;
            certaindays |= (OnTuesday || OnWorkingDays) ? CertainDays.Tuesday : 0;
            certaindays |= (OnWednesday || OnWorkingDays) ? CertainDays.Wednesday : 0;
            certaindays |= (OnThursday || OnWorkingDays) ? CertainDays.Thursday : 0;
            certaindays |= (OnFriday || OnWorkingDays) ? CertainDays.Friday : 0;
            certaindays |= (OnSaturday || OnWeekend) ? CertainDays.Saturday : 0;
            certaindays |= (OnSunday || OnWeekend) ? CertainDays.Sunday : 0;

            return new RecurrencyRule
            {
                UserEventId = this.Id,
                Recurrency = this.Recurrency ?? default,
                DayOfWeek = certaindays,
                WeekOfMonth = this.WeekOfMonth ?? default,
                EvenOdd = this.EvenOdd
            };
        }

        /// <summary>
        /// Map user event to viewmodel
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
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
                WeekOfMonth = userEvent.RecurrencyRule?.WeekOfMonth ?? default,
                OnMonday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Monday,
                OnTuesday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Tuesday,
                OnWednesday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Wednesday,
                OnThursday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Thursday,
                OnFriday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Friday,
                OnSaturday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Saturday,
                OnSunday = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Sunday,
                OnWeekend = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Saturday && userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Sunday,
                OnWorkingDays = userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Monday
                    && userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Tuesday
                    && userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Wednesday
                    && userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Thursday
                    && userEvent.RecurrencyRule?.DayOfWeek == CertainDays.Friday,
                EvenOdd = userEvent.RecurrencyRule?.EvenOdd ?? default
            };
        }
    }
}
