namespace CleanArchitecture.Application.Categories.Commands.CreateCommand;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.CategoryName)
            .MaximumLength(150).WithMessage(Messages.CategoryNameMaxLength)
            .MinimumLength(3).WithMessage(Messages.CategoryNameMinLength)
            .NotEmpty().WithMessage(Messages.CategoryNameNotNull);
    }
}
