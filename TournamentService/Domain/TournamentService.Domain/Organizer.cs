using TournamentService.Domain.Base;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Exceptions;
using TournamentService.ValueObjects;

namespace TournamentService.Domain;

public class Organizer : Entity<Guid>
{
    public UserName UserName { get; private set; }
    private readonly ICollection<Tournament> _tournaments = [];
    public IReadOnlyCollection<Tournament> Tournaments => _tournaments.ToList().AsReadOnly();

    public Organizer(UserName userName) : this(Guid.NewGuid(), userName)
    {
    }
    /// <summary>
    /// создается не для создания нового организатора, а для загрузки существующего из базы данных, поэтому id передается извне
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userName"></param>
    /// <exception cref="ArgumentNullValueException"></exception>
    protected Organizer(Guid id, UserName userName) : base(id)
    {
        UserName = userName ?? throw new ArgumentNullValueException(nameof(userName));
    }
    public Tournament CreateTournament(TournamentName tournamentName,
        Game game,
        DateTime startDate,
        DateTime endDate,
        TournamentFormat tournamentFormat,
        MaxTeams maxTeams,
        Money prizePool,
        Bracket bracket)
    {
        var tournament = new Tournament(this, tournamentName, game, startDate, endDate, tournamentFormat, maxTeams, prizePool, bracket);
        // не нужно проверять, что такого турнира уже нет, так как у турниров нет уникальных полей, кроме id, который генерируется внутри конструктора
        _tournaments.Add(tournament);

        return tournament;
    }
    public bool EditTournament(Tournament tournament, 
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
        if (tournament == null) throw new ArgumentNullValueException(nameof(tournament));
        if (!_tournaments.Contains(tournament)) return false;
        if(!tournament.Update(tournamentName, game, startDate, endDate, status, tournamentFormat, maxTeams, prizePool, bracket)) return false;
        return true;
    }


    public bool RemoveTournament(Tournament tournament)
    {
        if (tournament == null) throw new ArgumentNullValueException(nameof(tournament));
        if (!_tournaments.Contains(tournament)) return false;
        if (!tournament.CanBeCancelled(DateTime.UtcNow)) return false;
        return _tournaments.Remove(tournament);
    }
    

}
