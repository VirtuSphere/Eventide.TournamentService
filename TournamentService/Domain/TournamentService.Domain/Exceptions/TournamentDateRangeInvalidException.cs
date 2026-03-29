namespace TournamentService.Domain.Exceptions;

public class TournamentDateRangeInvalidException(DateTime startDate, DateTime endDate)
    : ArgumentException($"Invalid date range: start date '{startDate:O}' must be earlier than or equal to end date '{endDate:O}'.", "startDate");