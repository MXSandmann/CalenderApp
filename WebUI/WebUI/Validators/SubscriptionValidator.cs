using FluentValidation;
using WebUI.Models;

namespace WebUI.Validators
{
    public class SubscriptionValidator : AbstractValidator<CreateSubscriptionViewModel>
    {
        public SubscriptionValidator()
        {
            RuleFor(x => x.UserEmail).EmailAddress().NotEmpty().WithMessage("Your email is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Your name is required");
        }
    }
}
