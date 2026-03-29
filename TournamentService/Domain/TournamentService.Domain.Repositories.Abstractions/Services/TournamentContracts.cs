using TournamentService.Domain.Enumes;
using TournamentService.ValueObjects;

namespace TournamentService.Domain.Repositories.Abstractions.Services;

public sealed record CreateTournamentRequest(
    Guid OrganizerId,
    TournamentName TournamentName,
    Game Game,
    DateTime StartDate,
    DateTime EndDate,
    Status Status,
    TournamentFormat TournamentFormat,
    MaxTeams MaxTeams,
    Money PrizePool);

public sealed record UpdateTournamentRequest(
    TournamentName TournamentName,
    Game Game,
    DateTime StartDate,
    DateTime EndDate,
    Status Status,
    TournamentFormat TournamentFormat,
    MaxTeams MaxTeams,
    Money PrizePool,
    BracketId? BracketId);
