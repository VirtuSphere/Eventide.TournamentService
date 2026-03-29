using TournamentService.Domain;
using TournamentService.ValueObjects;

namespace TournamentService.Domain.Repositories.Abstractions.Services;

public interface IOrganizerService
{
    Task<Organizer> CreateAsync(
        UserName userName,
        CancellationToken cancellationToken = default);

    Task<Organizer?> GetByIdAsync(
        Guid organizerId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Organizer>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ChangeUserNameAsync(
        Guid organizerId,
        UserName userName,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid organizerId,
        CancellationToken cancellationToken = default);
}
