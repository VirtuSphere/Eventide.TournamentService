namespace TournamentService.Domain.Exceptions;

public class TournamentNameNullException()
    : ArgumentNullException("tournamentName", "Tournament name cannot be null.");