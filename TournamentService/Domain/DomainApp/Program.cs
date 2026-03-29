using TournamentService.Domain;
using TournamentService.ValueObjects;
using TournamentService.Domain.Enumes;
namespace DomainApp;
public class Program
{
    public static void Main(string[] args)
    {
        var org = new Organizer(Guid.NewGuid(), new UserName("John Doe"));
        org.CreateTournament(
            new TournamentName("Summer Cup"),
            new Game("Dota"),
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(7),
            Status.Draft,
            new TournamentFormat("local"),
            new MaxTeams(16),
            new Money(12333),
            bracketId: null);
        org.
        foreach (var tournament in org.Tournaments)
        {
            var bracketState = tournament.BracketId?.ToString() ?? "PENDING_FROM_BRACKET_SERVICE";
            Console.WriteLine($"{tournament.ToString()}");
        }

    }
}