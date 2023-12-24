using DietPenguin.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

using DietPenguin.Core.Services;
using DietPenguin.Domain.User;
namespace DietPenguin.Infrastructure.Database;

/// <summary>
/// Model bazy danych wysyłający <see cref="DietPenguin.Core.DomainEvent">zdarzenia domenowe</see> związane z encją
/// podczas zapisu zmian jej dotyczących.
/// </summary>
public class DispatchingContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUserService;
    
    public DispatchingContext(DbContextOptions<DispatchingContext> options, IMediator mediator, IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService) : base(options)
    {
        _mediator = mediator;
        _dateTimeProvider = dateTimeProvider;
        _currentUserService = currentUserService;
    }

    // ReSharper disable once RedundantOverriddenMember since this will be needed when adding any entity
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAuditFields();
        var result = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return result;
    }
    
    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }

    private void UpdateAuditFields()
    {
        var now = _dateTimeProvider.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.UpdateCreationProperties(now, _currentUserService.UserId);
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    break;
                
                case EntityState.Modified:
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    break;
                
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    entry.Entity.UpdateIsDeleted();
                    break;
            }
        }
    }
}