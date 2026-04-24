using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Contracts.Events;
using Eventide.TournamentService.Domain.Interfaces;
using MassTransit;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.CloseRegistration;

public class CloseRegistrationHandler : IRequestHandler<CloseRegistrationCommand, Result>
{
    private readonly ITournamentRepository _repo;
    private readonly IPublishEndpoint _publishEndpoint;

    public CloseRegistrationHandler(ITournamentRepository repo, IPublishEndpoint publishEndpoint)
    {
        _repo = repo;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(CloseRegistrationCommand req, CancellationToken ct)
    {
        var tournament = await _repo.GetByIdAsync(req.TournamentId, ct);
        if (tournament is null) return Result.Failure("Tournament not found");

        var participantIds = tournament.ParticipantIds.ToList();

        await _publishEndpoint.Publish(new RegistrationClosedEvent
        {
            TournamentId = tournament.Id,
            ParticipantIds = participantIds
        }, ct);

        return Result.Success();
    }
}