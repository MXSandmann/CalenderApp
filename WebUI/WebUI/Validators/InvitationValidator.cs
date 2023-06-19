using FluentValidation;
using WebUI.Models.ViewModels;

namespace WebUI.Validators
{
    public class InvitationValidator : AbstractValidator<CreateInvitationViewModel>
    {
        public InvitationValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Email is required");
        }
    }
}
