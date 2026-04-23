using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Domain.Enums;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.CreateTournament;

public class CreateTournamentCommand : IRequest<Result<Guid>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Game { get; init; } = string.Empty;
    public TournamentFormat Format { get; init; }
    public Guid OrganizerId { get; init; }
    public DateTime RegistrationStart { get; init; }
    public DateTime RegistrationEnd { get; init; }
    public DateTime TournamentStart { get; init; }
    public int MaxParticipants { get; init; }
    public int MaxTeamSize { get; init; } = 1;
}