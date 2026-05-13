using TournamentService.Domain.Base;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Exceptions;
using TournamentService.ValueObjects;
namespace TournamentService.Domain;

public class Tournament : Entity<Guid>
{
    public TournamentName TournamentName { get; private set; } = null!;
    public Organizer Organizer { get; private set; } = null!;
    public Game Game { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TournamentStatus Status { get; private set; }
    public TournamentFormat TournamentFormat { get; private set; }
    public MaxTeams MaxTeams { get; private set; } = null!;
    public Money PrizePool { get; private set; } = null!;
    public Bracket Bracket { get; private set; } = null!;
    public RegisteredTeamsCount RegisteredTeamsCount { get; private set; } = null!;

    
    protected Tournament() 
    {
    }

    public Tournament(Organizer organizer,
        TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        Bracket bracketId)
        : this(Guid.NewGuid(), organizer, tournamentName, game, startDate, endDate, TournamentStatus.Draft, tournamentFormat, maxTeams, prizePool, bracketId)
    {
    }

    protected Tournament(Guid id, Organizer organizer,
        TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        TournamentStatus status,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        Bracket bracket) : base(id)
    {
        Organizer = organizer ?? throw new ArgumentNullValueException(nameof(organizer));
        TournamentName = tournamentName ?? throw new ArgumentNullValueException(nameof(tournamentName));
        Game = game ?? throw new ArgumentNullValueException(nameof(game));
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        TournamentFormat = tournamentFormat;
        MaxTeams = maxTeams ?? throw new ArgumentNullValueException(nameof(maxTeams));
        PrizePool = prizePool ?? throw new ArgumentNullValueException(nameof(prizePool));
        Bracket= bracket ?? throw new ArgumentNullValueException(nameof(bracket));
        RegisteredTeamsCount = new RegisteredTeamsCount(0);

        ValidateDates(this, startDate, endDate);
    }
    public bool Update(
        TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        TournamentStatus status,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        Bracket bracket)
    {
        if (TournamentName == tournamentName &&
            Game == game &&
            StartDate == startDate &&
            EndDate == endDate &&
            Status == status &&
            TournamentFormat == tournamentFormat &&
            MaxTeams == maxTeams &&
            PrizePool == prizePool &&
            Bracket == bracket)
            return false;

        ValidateDates(this, startDate, endDate);
        ValidateStatusTransition(this, Status, status);

        TournamentName = tournamentName ?? throw new ArgumentNullValueException(nameof(tournamentName));
        Game = game ?? throw new ArgumentNullValueException(nameof(game));
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        TournamentFormat = tournamentFormat;
        MaxTeams = maxTeams ?? throw new ArgumentNullValueException(nameof(maxTeams));
        PrizePool = prizePool ?? throw new ArgumentNullValueException(nameof(prizePool));
        Bracket = bracket ?? throw new ArgumentNullValueException(nameof(bracket));
        return true;
    }

    public bool RegisterTeam()
    {
        if (Status != TournamentStatus.RegistrationOpen)
        {
            return false;
        }

        if (RegisteredTeamsCount.Value >= MaxTeams.Value)
        {
            return false;
        }

        RegisteredTeamsCount = RegisteredTeamsCount.Increment();
        return true;
    }

    public bool UnregisterTeam()
    {
        if (RegisteredTeamsCount.Value == 0)
        {
            return false;
        }

        RegisteredTeamsCount = RegisteredTeamsCount.Decrement();
        return true;
    }

    private static void ValidateDates(Tournament tournament, DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new TournamentDateRangeInvalidException(tournament, startDate, endDate);
        }
    }

    private static void ValidateStatusTransition(Tournament tournament, TournamentStatus currentStatus, TournamentStatus nextStatus)
    {
        if (currentStatus == nextStatus)
        {
            return;
        }

        var isValidTransition = currentStatus switch
        {
            TournamentStatus.Draft => nextStatus is TournamentStatus.RegistrationOpen,
            TournamentStatus.RegistrationOpen => nextStatus is TournamentStatus.RegistrationClosed,
            TournamentStatus.RegistrationClosed => nextStatus is TournamentStatus.InProgress,
            TournamentStatus.InProgress => nextStatus is TournamentStatus.Completed,
            TournamentStatus.Completed => false,
            _ => false
        };

        if (!isValidTransition)
        {
            throw new TournamentStatusTransitionInvalidException(tournament, currentStatus, nextStatus);
        }
    }
    
    public override string ToString()
    {
        return $"Tournament: {TournamentName}, Game: {Game}, StartDate: {StartDate}, EndDate: {EndDate}, Status: {Status}, TournamentFormat: {TournamentFormat}, MaxTeams: {MaxTeams}, PrizePool: {PrizePool}, RegisteredTeamsCount: {RegisteredTeamsCount.Value}, BracketId: {Bracket.Id}, OrganizerId: {Organizer.Id}";
    }
    public bool CanBeCancelled(DateTime now)
    {
        return Status == TournamentStatus.Draft &&
               RegisteredTeamsCount.Value == 0 &&
               StartDate - now >= TimeSpan.FromDays(7);
    }
}
