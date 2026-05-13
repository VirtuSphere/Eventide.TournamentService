using TournamentService.Domain;
using TournamentService.ValueObjects;
using TournamentService.Domain.Enumes;
namespace DomainApp;
public class Program
{
    public static void Main(string[] args)
    {
        var organizer = new Organizer(new UserName("John Doe"));
        var tournament = organizer.CreateTournament(
            new TournamentName("Summer Cup"),
            new Game("Dota 2"),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow.AddDays(16),
            TournamentFormat.SINGLE_ELIMINATION,
            new MaxTeams(16),
            new Money(12333),
            new Bracket(Guid.NewGuid()));

        Console.WriteLine("Created tournament:");
        Console.WriteLine(tournament);
        Console.WriteLine($"Can be cancelled now: {tournament.CanBeCancelled(DateTime.UtcNow)}");

        var updated = organizer.EditTournament(
            tournament,
            tournament.TournamentName,
            tournament.Game,
            tournament.StartDate,
            tournament.EndDate,
            TournamentStatus.RegistrationOpen,
            tournament.TournamentFormat,
            tournament.MaxTeams,
            tournament.PrizePool,
            tournament.Bracket);

        Console.WriteLine($"Updated to registration open: {updated}");
        Console.WriteLine(tournament);

        var registrationAdded = tournament.RegisterTeam();
        Console.WriteLine($"Registration added: {registrationAdded}");
        Console.WriteLine($"Can be cancelled after registration: {tournament.CanBeCancelled(DateTime.UtcNow)}");

        foreach (var currentTournament in organizer.Tournaments)
        {
            Console.WriteLine(currentTournament);
        }

    }
}