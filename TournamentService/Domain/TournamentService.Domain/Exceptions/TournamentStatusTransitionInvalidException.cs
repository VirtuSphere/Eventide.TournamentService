using TournamentService.Domain.Enumes;

namespace TournamentService.Domain.Exceptions;

public class TournamentStatusTransitionInvalidException(Status currentStatus, Status nextStatus)
    : InvalidOperationException($"Invalid tournament status transition: '{currentStatus}' -> '{nextStatus}'.");