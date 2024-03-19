namespace CleanArchitecture.Application.Categories.Commands.DeleteCommand;

public record DeleteCategoryCommand(int Id) : IRequest;

public class DeleteCategoryCommandHandler(ICategoryRepository _repository)
    : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        entity.AddDomainEvent(new CategoryDeletedEvent(entity));
        await _repository.DeleteAsync(entity);
    }
}