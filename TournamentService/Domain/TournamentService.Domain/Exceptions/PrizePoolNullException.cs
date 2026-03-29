namespace TournamentService.Domain.Exceptions;

public class PrizePoolNullException()
    : ArgumentNullException("prizePool", "Prize pool cannot be null.");