using FluentValidation;
using TaskManagerApi.DTOs.Tags;

namespace TaskManagerApi.Validators.Tags
{
    public class TagCreateDtoValidator : AbstractValidator<TagCreateDto>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");
        }
    }
}
