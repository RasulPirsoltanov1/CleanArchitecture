using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitecture.Infrastructure.Data.Interceptors;
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _dateTime;
    private readonly IUser _user;
    IMediator _mediator;
    public AuditableEntityInterceptor(TimeProvider dateTime, IUser user, IMediator mediator)
    {
        this._dateTime = dateTime;
        this._user = user;
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);

        UpdateEntities(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.Created = _dateTime.GetUtcNow();
                    entry.Entity.CreatedBy = _user.Id ?? "CodeEdu";
                }
                entry.Entity.LastModified = _dateTime.GetUtcNow();
                entry.Entity.LastModifiedBy = _user.Id ?? "CodeEdu";
            }
        }
    }
    public async Task DispatchDomainEvents(DbContext? dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
        var entities = dbContext.ChangeTracker.Entries<BaseEntity>().Where(e => e.Entity.DomainEvents.Any()).Select(x => x.Entity);
        var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();
        entities.ToList().ForEach(e => e.ClearDomainEvents());
        foreach (var entity in domainEvents)
        {
            await _mediator.Publish(entity);
        }
    }
}
