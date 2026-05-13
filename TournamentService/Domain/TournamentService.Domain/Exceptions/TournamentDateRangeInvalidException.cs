
namespace TournamentService.Domain.Exceptions;

public class TournamentDateRangeInvalidException : ArgumentException
{
    public Tournament Tournament { get; }

    public TournamentDateRangeInvalidException(Tournament tournament, DateTime startDate, DateTime endDate)
        : base($"Invalid date range for tournament '{tournament.TournamentName}' (Id: {tournament.Id}): start date '{startDate:O}' must be earlier than or equal to end date '{endDate:O}'.", nameof(startDate))
    {
        Tournament = tournament;
    }
}