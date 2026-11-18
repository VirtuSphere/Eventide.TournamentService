using TournamentService.Application.Models.Base;
using TournamentService.Domain.Enumes;

namespace TournamentService.Application.Models.Tournament;

public record class CreateTournamentModel(
    Guid OrganizerId,
    string TournamentName,
    string Game,
    DateTime StartDate,
    DateTime EndDate,
    TournamentFormat TournamentFormat,
    byte MaxTeams,
    decimal PrizePool,
    Guid BracketId) : ICreateModel;
