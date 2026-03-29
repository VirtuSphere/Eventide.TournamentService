namespace TournamentService.Domain.Exceptions;

public class TournamentNullException()
    : ArgumentNullException("tournament", "Tournament cannot be null.");