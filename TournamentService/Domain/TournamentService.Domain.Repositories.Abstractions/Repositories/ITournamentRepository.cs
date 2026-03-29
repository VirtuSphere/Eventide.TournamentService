using TournamentService.Domain;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Repositories.Abstractions.Base;

namespace TournamentService.Domain.Repositories.Abstractions.Repositories;

public interface ITournamentRepository : IRepository<Tournament, Guid>
{
    Task<IReadOnlyCollection<Tournament>> GetByOrganizerIdAsync(
        Guid organizerId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Tournament>> GetByStatusAsync(
        Status status,
        CancellationToken cancellationToken = default);
}
