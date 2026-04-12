using FluentValidation;
using TaskManagerApi.DTOs.Categories;

namespace TaskManagerApi.Validators.Categories
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .When(x => x.Name != null)
                .WithMessage("Name must be at least 2 characters long.");
        }
    }
}
