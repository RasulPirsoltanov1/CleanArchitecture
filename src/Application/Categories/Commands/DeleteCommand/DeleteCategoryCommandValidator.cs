namespace CleanArchitecture.Application.Categories.Commands.DeleteCommand;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithMessage(Messages.CategoryIdNotNull)
            .Must(LessThanOrEqualTo0).WithMessage(Messages.CategoryIdLessThanOrEqualTo0);

    }
     
    private bool LessThanOrEqualTo0(int id)
    {
        return id > 0 && id != 0;
    } 
}
