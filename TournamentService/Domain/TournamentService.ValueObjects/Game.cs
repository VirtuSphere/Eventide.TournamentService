using TournamentService.ValueObjects.Validators;
using TournamentService.ValueObjects.Base;

namespace TournamentService.ValueObjects;

public class Game(string name) : ValueObject<string>(new GameValidator(), name);