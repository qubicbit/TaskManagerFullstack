using FluentValidation;
using TaskManagerApi.DTOs.Tags;

namespace TaskManagerApi.Validators.Tags
{
    public class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
    {
        public TagUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .When(x => x.Name != null)
                .WithMessage("Name must be at least 2 characters long.");
        }
    }
}
