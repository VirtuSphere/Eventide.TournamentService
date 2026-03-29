using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Exceptions;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.ValueObjects;

public class TournamentFormat(string name) : ValueObject<string>(new TournamentFormatValidator(), name);
