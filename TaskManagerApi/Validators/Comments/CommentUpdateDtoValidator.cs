using FluentValidation;
using TaskManagerApi.DTOs.Comments;

namespace TaskManagerApi.Validators.Comments
{
    public class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateDtoValidator()
        {
            RuleFor(x => x.Content)
                .MinimumLength(2)
                .When(x => x.Content != null)
                .WithMessage("text must be at least 2 characters long.");
        }
    }
}
