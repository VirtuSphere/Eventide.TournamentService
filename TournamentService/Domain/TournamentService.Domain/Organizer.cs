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

public class Organizer : Entity<Guid>
{
    public UserName UserName { get; private set; }
    private readonly List<Tournament> _tournaments = new();
    public IReadOnlyCollection<Tournament> Tournaments => _tournaments.AsReadOnly();
    public Organizer(Guid id, UserName userName) : base(id)
    {
        UserName = userName ?? throw new OrganizerUserNameNullException();
    }
    public bool ChangeUserName(UserName userName)
    {
        if (userName == null) throw new OrganizerUserNameNullException();
        if (UserName == userName) return false;
        UserName = userName;
        return true;
    }
    public Tournament CreateTournament(TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        Status status,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        BracketId? bracketId)
    {
        var tournament = new Tournament(Guid.NewGuid(), tournamentName, game, startDate, endDate, status, tournamentFormat, maxTeams, prizePool, bracketId);
        _tournaments.Add(tournament);
        return tournament;
    }
    public bool EditTournament(Tournament tournament, 
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
        if (tournament == null) throw new TournamentNullException();
        if (!_tournaments.Contains(tournament)) return false;
        tournament.Update(tournamentName, game, startDate, endDate, status, tournamentFormat, maxTeams, prizePool, bracketId);
        return true;
    }


    public bool RemoveTournament(Tournament tournament)
    {
        if (tournament == null) throw new TournamentNullException();
        return _tournaments.Remove(tournament);
    }
    

}
