using Microsoft.EntityFrameworkCore;
using TournamentService.Domain;
using TournamentService.Domain.Base;
using TournamentService.Domain.Repositories.Abstractions.Base;
namespace TournamentService.Infrastructure.EntityFramework.RepositoriesEF;
public class EfRepository<TEntity, TId>(ApplicationDbContext context)
        : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
{
    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Set<TEntity>()
        .ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        => await context.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        => await context.Set<TEntity>().AnyAsync(entity => entity.Id.Equals(id), cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}