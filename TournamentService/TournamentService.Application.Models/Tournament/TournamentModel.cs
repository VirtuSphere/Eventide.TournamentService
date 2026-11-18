using TournamentService.Application.Models.Base;
using TournamentService.Domain.Enumes;

namespace TournamentService.Application.Models.Tournament;

public sealed record TournamentModel(
    Guid Id,
    Guid OrganizerId,
    string TournamentName,
    string Game,
    DateTime StartDate,
    DateTime EndDate,
    TournamentStatus Status,
    TournamentFormat TournamentFormat,
    byte MaxTeams,
    decimal PrizePool,
    Guid BracketId,
    byte RegisteredTeamsCount) : IModel<Guid>;
