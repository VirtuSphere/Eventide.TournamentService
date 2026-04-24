using Eventide.TournamentService.Application.Common;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.CloseRegistration;

public class CloseRegistrationCommand : IRequest<Result>
{
    public Guid TournamentId { get; init; }
}