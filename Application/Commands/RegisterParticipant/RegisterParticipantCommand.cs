using Eventide.TournamentService.Application.Common;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.RegisterParticipant;

public class RegisterParticipantCommand : IRequest<Result>
{
    public Guid TournamentId { get; init; }
    public Guid UserId { get; init; }
}