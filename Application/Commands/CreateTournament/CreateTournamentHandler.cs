using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Domain.Entities;
using Eventide.TournamentService.Domain.Interfaces;
using MediatR;

namespace Eventide.TournamentService.Application.Commands.CreateTournament;

public class CreateTournamentHandler : IRequestHandler<CreateTournamentCommand, Result<Guid>>
{
    private readonly ITournamentRepository _repo;

    public CreateTournamentHandler(ITournamentRepository repo) => _repo = repo;

    public async Task<Result<Guid>> Handle(CreateTournamentCommand req, CancellationToken ct)
    {
        if (await _repo.ExistsByNameAsync(req.Name, ct))
            return Result<Guid>.Failure("Tournament with this name already exists");

        var tournament = Tournament.Create(req.Name, req.Game, req.Format, req.OrganizerId,
            req.RegistrationStart, req.RegistrationEnd, req.TournamentStart,
            req.MaxParticipants, req.MaxTeamSize);

        await _repo.AddAsync(tournament, ct);
        await _repo.SaveChangesAsync(ct);

        return Result<Guid>.Success(tournament.Id);
    }
}