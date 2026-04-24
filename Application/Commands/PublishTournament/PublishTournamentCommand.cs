using Eventide.TournamentService.Application.Common;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.PublishTournament;

public class PublishTournamentCommand : IRequest<Result>
{
    public Guid TournamentId { get; init; }
}