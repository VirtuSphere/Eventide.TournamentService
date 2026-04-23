using Eventide.TournamentService.Application.Common;
using Eventide.TournamentService.Application.DTOs;
using MediatR;

namespace Eventide.TournamentService.Application.Queries.GetUpcomingTournaments;

public class GetUpcomingTournamentsQuery : IRequest<Result<List<TournamentDto>>>
{
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 20;
}