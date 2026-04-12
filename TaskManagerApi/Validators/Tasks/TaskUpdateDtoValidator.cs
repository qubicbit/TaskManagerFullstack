using FluentValidation;
using TaskManagerApi.DTOs.Tasks;

namespace TaskManagerApi.Validators.Tasks
{
    public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .When(x => x.Title != null)
                .WithMessage("Title must be at least 3 characters long.");

            RuleFor(x => x.Description)
                .MinimumLength(5)
                .When(x => x.Description != null)
                .WithMessage("Description must be at least 5 characters long.");

            RuleFor(x => x.IsCompleted)
                .NotNull()
                .When(x => x.IsCompleted.HasValue)
                .WithMessage("IsCompleted must be true or false.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .When(x => x.CategoryId.HasValue)
                .WithMessage("CategoryId must be greater than 0.");

            When(x => x.TagIds != null, () =>
            {
                RuleForEach(x => x.TagIds)
                    .GreaterThan(0)
                    .WithMessage("TagIds must contain valid tag IDs.");
            });
        }
    }
}
