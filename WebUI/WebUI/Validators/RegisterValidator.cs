using FluentValidation;
using WebUI.Models.ViewModels;

namespace WebUI.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password ist required");

            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .WithMessage("Plese confirm your password");
            
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                .WithMessage("Passwords doesn't match");

            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
