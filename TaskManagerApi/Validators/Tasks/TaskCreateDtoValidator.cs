using FluentValidation;
using TaskManagerApi.DTOs.Tasks;

namespace TaskManagerApi.Validators.Tasks
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(5).WithMessage("Description must be at least 5 characters long.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");

            RuleForEach(x => x.TagIds)
                .GreaterThan(0)
                .WithMessage("TagIds must contain valid tag IDs.");
        }
    }
}
