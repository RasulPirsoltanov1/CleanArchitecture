//using MediatR;
//using Microsoft.EntityFrameworkCore.Diagnostics;

//namespace CleanArchitecture.Infrastructure.Data.Interceptors;

//public class DispatchDomainEventInterceptor(IMediator _mediator) : SaveChangesInterceptor
//{

//    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
//    {
//        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
//        return base.SavedChanges(eventData, result);
//    }
//    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
//    {
//        await DispatchDomainEvents(eventData.Context);
//        return await base.SavedChangesAsync(eventData, result, cancellationToken);
//    }

//    public async Task DispatchDomainEvents(DbContext? dbContext)
//    {
//        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
//        var entities = dbContext.ChangeTracker.Entries<BaseEntity>().Where(e => e.Entity.DomainEvents.Any()).Select(x => x.Entity);
//        var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();
//        entities.ToList().ForEach(e => e.ClearDomainEvents());
//        foreach (var entity in domainEvents)
//        {
//            await _mediator.Publish(entity);   
//        }
//    }
//}