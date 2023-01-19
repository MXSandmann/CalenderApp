using FluentValidation;
using WebUI.Models;

namespace WebUI.Validators
{
    public class DateValidator : AbstractValidator<UserEventViewModel>
    {
        public DateValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time is Required");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required")
                .GreaterThan(x => x.StartTime)
                                .WithMessage("End time must after Start time");
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.LastDate)
                .GreaterThan(x => x.Date)
                .WithMessage("Last date time must after date");

        }
    }
}
