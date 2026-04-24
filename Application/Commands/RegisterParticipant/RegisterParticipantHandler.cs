using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Domain.Exceptions;
using Eventide.TournamentService.Domain.Interfaces;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.RegisterParticipant;

public class RegisterParticipantHandler : IRequestHandler<RegisterParticipantCommand, Result>
{
    private readonly ITournamentRepository _repo;

    public RegisterParticipantHandler(ITournamentRepository repo) => _repo = repo;

    public async Task<Result> Handle(RegisterParticipantCommand req, CancellationToken ct)
    {
        var tournament = await _repo.GetByIdAsync(req.TournamentId, ct);
        if (tournament is null) return Result.Failure("Tournament not found");

        try
        {
            tournament.RegisterParticipant(req.UserId);
            await _repo.UpdateAsync(tournament, ct);
            await _repo.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}