using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Exceptions;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.ValueObjects;

public class TournamentName(string name) : ValueObject<string>(new TournamentNameValidator(), name);
