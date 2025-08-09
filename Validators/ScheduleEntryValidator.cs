using FluentValidation;

namespace Library.Validators
{
    public class ScheduleEntryValidator : AbstractValidator<ScheduleEntry>
    {
        public ScheduleEntryValidator() 
        {
            RuleFor(entry => entry.Title)
                .NotEmpty()
                .WithMessage("A Title is required for the schedule entry.");

            RuleFor(entry => entry.StartDateTime)
                .NotEmpty()
                .WithMessage("start time is required");


        }
    }
}
