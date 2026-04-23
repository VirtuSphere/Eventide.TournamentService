using Eventide.TournamentService.Domain.Entities;

namespace Eventide.TournamentService.Domain.Interfaces;

public interface ITournamentRepository
{
    Task<Tournament?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Tournament>> GetUpcomingAsync(int skip, int take, CancellationToken ct = default);
    Task<List<Tournament>> GetByOrganizerAsync(Guid organizerId, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    Task AddAsync(Tournament tournament, CancellationToken ct = default);
    Task UpdateAsync(Tournament tournament, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}