namespace TournamentService.Domain.Repositories.Abstractions.Services;

public interface IBracketServiceClient
{
    Task<Guid?> TryCreateBracketAsync(
        Guid tournamentId,
        string tournamentFormat,
        byte maxTeams,
        CancellationToken cancellationToken = default);
}
