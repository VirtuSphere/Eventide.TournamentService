using TournamentService.Domain;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Exceptions;
using TournamentService.ValueObjects;

namespace TournamentService.Tests;

public sealed class DomainEdgeTests
{
    [Fact]
    public void Organizer_change_username_updates_value()
    {
        var organizer = new Organizer(new UserName("before"));

        organizer.ChangeUserName(new UserName("after"));

        Assert.Equal("after", organizer.UserName.Value);
    }

    [Fact]
    public void Organizer_rejects_null_name_and_tournament_operations()
    {
        var organizer = new Organizer(new UserName("organizer"));
        var tournament = CreateTournament();

        Assert.Throws<ArgumentNullValueException>(() => organizer.ChangeUserName(null!));
        Assert.Throws<ArgumentNullValueException>(() => organizer.EditTournament(
            null!, tournament.TournamentName, tournament.Game,
            tournament.StartDate, tournament.EndDate, tournament.Status,
            tournament.TournamentFormat, tournament.MaxTeams,
            tournament.PrizePool, tournament.Bracket));
        Assert.Throws<ArgumentNullValueException>(() => organizer.RemoveTournament(null!));
    }

    [Fact]
    public void Tournament_rejects_null_update_values()
    {
        var tournament = CreateTournament();

        Assert.Throws<ArgumentNullValueException>(() => tournament.Update(
            null!, tournament.Game, tournament.StartDate, tournament.EndDate,
            tournament.Status, tournament.TournamentFormat,
            tournament.MaxTeams, tournament.PrizePool, tournament.Bracket));
    }

    [Fact]
    public void Tournament_supports_registered_status_transitions()
    {
        var tournament = CreateTournament();

        Assert.True(tournament.Update(
            tournament.TournamentName, tournament.Game,
            tournament.StartDate, tournament.EndDate,
            TournamentStatus.RegistrationOpen,
            tournament.TournamentFormat, tournament.MaxTeams,
            tournament.PrizePool, tournament.Bracket));
        Assert.True(tournament.Update(
            tournament.TournamentName, tournament.Game,
            tournament.StartDate, tournament.EndDate,
            TournamentStatus.RegistrationClosed,
            tournament.TournamentFormat, tournament.MaxTeams,
            tournament.PrizePool, tournament.Bracket));
        Assert.True(tournament.Update(
            tournament.TournamentName, tournament.Game,
            tournament.StartDate, tournament.EndDate,
            TournamentStatus.InProgress,
            tournament.TournamentFormat, tournament.MaxTeams,
            tournament.PrizePool, tournament.Bracket));
        Assert.True(tournament.Update(
            tournament.TournamentName, tournament.Game,
            tournament.StartDate, tournament.EndDate,
            TournamentStatus.Completed,
            tournament.TournamentFormat, tournament.MaxTeams,
            tournament.PrizePool, tournament.Bracket));
    }

    [Fact]
    public void Tournament_cancellation_requires_draft_empty_tournament()
    {
        var tournament = CreateTournament();

        Assert.True(tournament.CanBeCancelled(DateTime.UtcNow));
    }

    [Fact]
    public void Money_rejects_values_above_limit()
    {
        Assert.ThrowsAny<Exception>(() => new Money(1_000_000m));
    }

    [Fact]
    public void Basic_value_objects_support_round_trip_and_null_validation()
    {
        Assert.Equal("Chess", new Game("Chess").ToString());
        Assert.Equal("Cup", new TournamentName("Cup").ToString());
        Assert.Equal("organizer", new UserName("organizer").ToString());

        Assert.ThrowsAny<Exception>(() => new TournamentName(""));
        Assert.ThrowsAny<Exception>(() => new Game("A"));
        Assert.ThrowsAny<Exception>(() => new UserName(""));
    }

    private static Tournament CreateTournament()
        => new(
            new Organizer(new UserName("organizer")),
            new TournamentName("Cup"),
            new Game("Chess"),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow.AddDays(11),
            TournamentFormat.SWISS,
            new MaxTeams(8),
            new Money(100),
            new Bracket(Guid.NewGuid()));
}
