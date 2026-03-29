using TournamentService.Domain;

namespace TournamentService.Domain.Repositories.Abstractions.Services;

public interface ITournamentService
{
    Task<Tournament> CreateAsync(
        CreateTournamentRequest request,
        CancellationToken cancellationToken = default);

    Task<Tournament?> GetByIdAsync(
        Guid tournamentId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Tournament>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(
        Guid tournamentId,
        UpdateTournamentRequest request,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid tournamentId,
        CancellationToken cancellationToken = default);

    Task<bool> EnsureBracketCreatedAsync(
        Guid tournamentId,
        CancellationToken cancellationToken = default);
}
