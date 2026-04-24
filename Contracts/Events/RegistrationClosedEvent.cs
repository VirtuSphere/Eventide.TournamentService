namespace Eventide.TournamentService.Contracts.Events;

public class RegistrationClosedEvent
{
    public Guid TournamentId { get; init; }
    public List<Guid> ParticipantIds { get; init; } = new();
}