﻿using ApplicationCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class RecurrencyRule
    {
        [Key]
        public Guid Id { get; set; }
        public UserEvent UserEvent { get; set; } = null!;
        public Guid UserEventId { get; set; }
        /// <summary>
        /// Classic recurrency - daily, weekly, monthly and yearly
        /// </summary>
        public Recurrency Recurrency { get; set; }
        /// <summary>
        /// Occurrency for certain day, bit coded
        /// </summary>        
        public CertainDays DayOfWeek { get; set; }
        public WeekOfTheMonth WeekOfMonth { get; set; }
        public EvenOdd EvenOdd { get; set; }
    }
}
