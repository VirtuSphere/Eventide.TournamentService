namespace TournamentService.Domain.Exceptions;

public class MaxTeamsNullException()
    : ArgumentNullException("maxTeams", "Max teams cannot be null.");