using CleanArchitecture.Application.Categories.Commands.CreateCommand;

namespace CleanArchitecture.Application.Categories.EventHandlers;
public class CategoryCreateEventHandler(ILogger<CreaCreateCategoryCommandHandler> _logger)
    : INotificationHandler<CategoryCreatedEvent>
{
    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogError("CleanArchitecture Domain Event : {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
