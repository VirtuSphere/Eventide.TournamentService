using TournamentService.Application.Models.Base;

namespace TournamentService.Application.Models.Bracket;

public record class CreateBracketModel(Guid Id) : ICreateModel;
