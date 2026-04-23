using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Application.DTOs;
using Eventide.TournamentService.Domain.Interfaces;
using MediatR;

namespace Eventide.TournamentService.Application.Queries.GetUpcomingTournaments;

public class GetUpcomingTournamentsHandler : IRequestHandler<GetUpcomingTournamentsQuery, Result<List<TournamentDto>>>
{
    private readonly ITournamentRepository _repo;

    public GetUpcomingTournamentsHandler(ITournamentRepository repo) => _repo = repo;

    public async Task<Result<List<TournamentDto>>> Handle(GetUpcomingTournamentsQuery req, CancellationToken ct)
    {
        var tournaments = await _repo.GetUpcomingAsync(req.Skip, req.Take, ct);

        var dtos = tournaments.Select(t => new TournamentDto
        {
            Id = t.Id,
            Name = t.Name,
            Game = t.Game,
            Format = t.Format.ToString(),
            Status = t.Status.ToString(),
            ParticipantCount = t.ParticipantIds.Count,
            MaxParticipants = t.MaxParticipants,
            TournamentStart = t.TournamentStart
        }).ToList();

        return Result<List<TournamentDto>>.Success(dtos);
    }
}