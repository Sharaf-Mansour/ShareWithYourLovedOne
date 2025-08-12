using FluentValidation;
namespace ShareWithYourLovedOne.Validators;
public class OwnerValidator : AbstractValidator<Owner>
{
    public OwnerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Full Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name is too long.");


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address cannot be empty.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.");

        RuleSet("Login", () =>
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address cannot be empty.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");
        });
    }
}