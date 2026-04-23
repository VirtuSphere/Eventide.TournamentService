namespace Eventide.TournamentService.Application.DTOs;

public class TournamentDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Game { get; init; } = string.Empty;
    public string Format { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public int ParticipantCount { get; init; }
    public int MaxParticipants { get; init; }
    public DateTime TournamentStart { get; init; }
}