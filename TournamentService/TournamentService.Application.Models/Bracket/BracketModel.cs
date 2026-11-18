using TournamentService.Application.Models.Base;

namespace TournamentService.Application.Models.Bracket;

public record class BracketModel(Guid Id) : IModel<Guid>;
