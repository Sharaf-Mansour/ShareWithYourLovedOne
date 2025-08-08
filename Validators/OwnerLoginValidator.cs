using FluentValidation;
namespace Library.Validators
{
    public class OwnerLoginValidator : AbstractValidator<Owner>
    {
        public OwnerLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address cannot be empty.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
