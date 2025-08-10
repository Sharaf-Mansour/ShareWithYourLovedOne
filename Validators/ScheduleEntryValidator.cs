using FluentValidation;

namespace ShareWithYourLovedOne.Validators
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

            RuleFor(entry => entry.EndDateTime)
                .NotEmpty()
                .WithMessage("End Time is required");

            RuleFor(entry => entry.EndDateTime)
                .GreaterThan(entry => entry.StartDateTime)
                .WithMessage("End Time must be after Start time");
        }
    }
}
