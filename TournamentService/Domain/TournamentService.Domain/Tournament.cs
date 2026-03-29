using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentService.Domain.Base;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Exceptions;
using TournamentService.ValueObjects;
namespace TournamentService.Domain;

public class Tournament : Entity<Guid>
{
    public TournamentName TournamentName { get; private set; } = null!;
    public Game Game { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Status Status { get; private set; }
    public TournamentFormat TournamentFormat { get; private set; } = null!;
    public MaxTeams MaxTeams { get; private set; } = null!;
    public Money PrizePool { get; private set; } = null!;
    public BracketId? BracketId { get; private set; }

    
    protected Tournament() 
    {
    }
    public Tournament(Guid id, 
        TournamentName tournamentName,
        Game game, 
        DateTime startDate, 
        DateTime endDate, 
        Status status, 
        TournamentFormat tournamentFormat, 
        MaxTeams maxTeams, 
        Money prizePool, 
        BracketId? bracketId) : base(id)
    {
        ValidateDates(startDate, endDate);

        TournamentName = tournamentName ?? throw new TournamentNameNullException();
        Game = game ?? throw new TournamentNameNullException(); ;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        TournamentFormat = tournamentFormat ?? throw new TournamentFormatNullException();
        MaxTeams = maxTeams ?? throw new MaxTeamsNullException();
        PrizePool = prizePool ?? throw new PrizePoolNullException();
        BracketId = bracketId;
    }
    public bool Update(
        TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        Status status,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        BracketId? bracketId)
    {
        if (TournamentName == tournamentName &&
            Game == game &&
            StartDate == startDate &&
            EndDate == endDate &&
            Status == status &&
            TournamentFormat == tournamentFormat &&
            MaxTeams == maxTeams &&
            PrizePool == prizePool &&
            BracketId == bracketId)
            return false;

        ValidateDates(startDate, endDate);
        ValidateStatusTransition(Status, status);

        TournamentName = tournamentName ?? throw new TournamentNameNullException();
        Game = game;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        TournamentFormat = tournamentFormat ?? throw new TournamentFormatNullException();
        MaxTeams = maxTeams ?? throw new MaxTeamsNullException();
        PrizePool = prizePool ?? throw new PrizePoolNullException();
        BracketId = bracketId;
        return true;
    }

    private static void ValidateDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new TournamentDateRangeInvalidException(startDate, endDate);
        }
    }

    private static void ValidateStatusTransition(Status currentStatus, Status nextStatus)
    {
        if (currentStatus == nextStatus)
        {
            return;
        }

        var isValidTransition = currentStatus switch
        {
            Status.Draft => nextStatus is Status.RegistrationOpen,
            Status.RegistrationOpen => nextStatus is Status.RegistrationClosed,
            Status.RegistrationClosed => nextStatus is Status.InProgress,
            Status.InProgress => nextStatus is Status.Completed,
            Status.Completed => false,
            _ => false
        };

        if (!isValidTransition)
        {
            throw new TournamentStatusTransitionInvalidException(currentStatus, nextStatus);
        }
    }
    
    public override string ToString()
    {
        return $"Tournament: {TournamentName}, Game: {Game}, StartDate: {StartDate}, EndDate: {EndDate}, Status: {Status}, TournamentFormat: {TournamentFormat}, MaxTeams: {MaxTeams}, PrizePool: {PrizePool}, BracketId: {BracketId}";
    }
}
