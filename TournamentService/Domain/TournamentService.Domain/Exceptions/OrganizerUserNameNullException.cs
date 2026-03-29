namespace TournamentService.Domain.Exceptions;

public class OrganizerUserNameNullException()
    : ArgumentNullException("userName", "Organizer user name cannot be null.");