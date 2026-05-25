

namespace TournamentService.Domain.Exceptions;

public class TournamentNotFoundException : Exception
{
    public TournamentNotFoundException(Guid tournamentId)
        : base($"Tournament with ID '{tournamentId}' was not found.")
    {
    }
}
