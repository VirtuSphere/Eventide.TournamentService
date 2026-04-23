namespace Eventide.TournamentService.Contracts.Events;

public class TournamentCreatedEvent
{
    public Guid TournamentId { get; init; }
    public string TournamentName { get; init; } = string.Empty;
    public string Game { get; init; } = string.Empty;
    public Guid OrganizerId { get; init; }
}