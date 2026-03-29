namespace TournamentService.Domain.Exceptions;

public class TournamentFormatNullException()
    : ArgumentNullException("tournamentFormat", "Tournament format cannot be null.");