using FluentValidation;
using TaskManagerApi.DTOs.Comments;

namespace TaskManagerApi.Validators.Comments
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Text is required.")
                .MinimumLength(2).WithMessage("Text must be at least 2 characters long.");


        }
    }
}
