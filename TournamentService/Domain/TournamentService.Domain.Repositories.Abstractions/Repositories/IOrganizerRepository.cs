using TournamentService.Domain;
using TournamentService.Domain.Repositories.Abstractions.Base;
using TournamentService.ValueObjects;

namespace TournamentService.Domain.Repositories.Abstractions.Repositories;

public interface IOrganizerRepository : IRepository<Organizer, Guid>
{
    Task<Organizer?> GetByUserNameAsync(
        UserName userName,
        CancellationToken cancellationToken = default);
}
