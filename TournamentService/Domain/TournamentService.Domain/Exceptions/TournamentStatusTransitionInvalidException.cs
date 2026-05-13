using TournamentService.Domain.Enumes;

namespace TournamentService.Domain.Exceptions;

public class TournamentStatusTransitionInvalidException : InvalidOperationException
{
    public Tournament Tournament { get; }

    public TournamentStatusTransitionInvalidException(Tournament tournament, TournamentStatus currentStatus, TournamentStatus nextStatus)
        : base($"Invalid tournament status transition for '{tournament.TournamentName}' (Id: {tournament.Id}): '{currentStatus}' -> '{nextStatus}'.")
    {
        Tournament = tournament;
    }
}