using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Domain.Interfaces;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.PublishTournament;

public class PublishTournamentHandler : IRequestHandler<PublishTournamentCommand, Result>
{
    private readonly ITournamentRepository _repo;

    public PublishTournamentHandler(ITournamentRepository repo) => _repo = repo;

    public async Task<Result> Handle(PublishTournamentCommand req, CancellationToken ct)
    {
        var tournament = await _repo.GetByIdAsync(req.TournamentId, ct);
        if (tournament is null) return Result.Failure("Tournament not found");

        tournament.Publish();
        await _repo.SaveChangesAsync(ct);
        return Result.Success();
    }
}