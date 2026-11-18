using TournamentService.Domain;
using TournamentService.Domain.Repositories.Abstractions.Repositories;
using TournamentService.ValueObjects;

namespace TournamentService.Tests;

internal sealed class FakeOrganizerRepository : IOrganizerRepository
{
    private readonly Dictionary<Guid, Organizer> _organizers = [];

    public IReadOnlyCollection<Organizer> Items => _organizers.Values.ToList();

    public Task<IReadOnlyCollection<Organizer>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyCollection<Organizer>>(_organizers.Values.ToList());

    public Task<Organizer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_organizers.GetValueOrDefault(id));

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_organizers.ContainsKey(id));

    public Task AddAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        _organizers[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        _organizers[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Organizer entity, CancellationToken cancellationToken = default)
    {
        _organizers.Remove(entity.Id);
        return Task.CompletedTask;
    }

    public Task<Organizer?> GetOrganizerByUsernameAsync(
        UserName username,
        CancellationToken cancellationToken = default)
        => Task.FromResult(_organizers.Values
            .FirstOrDefault(organizer => organizer.UserName == username));
}
