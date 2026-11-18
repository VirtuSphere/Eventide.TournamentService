using TournamentService.Domain;
using TournamentService.Domain.Enumes;
using TournamentService.Domain.Exceptions;
using TournamentService.ValueObjects;

namespace TournamentService.Tests;

public sealed class TournamentTests
{
    private static Tournament CreateTournament(
        TournamentStatus status = TournamentStatus.Draft,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var tournament = new Tournament(
            new Organizer(new UserName("organizer")),
            new TournamentName("Autumn Cup"),
            new Game("Chess"),
            startDate ?? DateTime.UtcNow.AddDays(10),
            endDate ?? DateTime.UtcNow.AddDays(11),
            TournamentFormat.SINGLE_ELIMINATION,
            new MaxTeams(8),
            new Money(100),
            new Bracket(Guid.NewGuid()));

        if (status != TournamentStatus.Draft)
        {
            tournament.Update(
                tournament.TournamentName,
                tournament.Game,
                tournament.StartDate,
                tournament.EndDate,
                status,
                tournament.TournamentFormat,
                tournament.MaxTeams,
                tournament.PrizePool,
                tournament.Bracket);
        }

        return tournament;
    }

    [Fact]
    public void Constructor_creates_draft_tournament()
    {
        var tournament = CreateTournament();

        Assert.Equal(TournamentStatus.Draft, tournament.Status);
        Assert.Equal((byte)0, tournament.RegisteredTeamsCount.Value);
        Assert.Contains("Tournament:", tournament.ToString());
    }

    [Fact]
    public void Constructor_rejects_end_date_before_start_date()
    {
        Assert.Throws<TournamentDateRangeInvalidException>(() =>
            new Tournament(
                new Organizer(new UserName("organizer")),
                new TournamentName("Cup"),
                new Game("Chess"),
                DateTime.UtcNow.AddDays(2),
                DateTime.UtcNow.AddDays(1),
                TournamentFormat.SWISS,
                new MaxTeams(8),
                new Money(100),
                new Bracket(Guid.NewGuid())));
    }

    [Fact]
    public void Update_applies_valid_status_transition()
    {
        var tournament = CreateTournament();

        var updated = tournament.Update(
            tournament.TournamentName,
            tournament.Game,
            tournament.StartDate,
            tournament.EndDate,
            TournamentStatus.RegistrationOpen,
            tournament.TournamentFormat,
            tournament.MaxTeams,
            tournament.PrizePool,
            tournament.Bracket);

        Assert.True(updated);
        Assert.Equal(TournamentStatus.RegistrationOpen, tournament.Status);
    }

    [Fact]
    public void Update_rejects_invalid_status_transition()
    {
        var tournament = CreateTournament();

        Assert.Throws<TournamentStatusTransitionInvalidException>(() =>
            tournament.Update(
                tournament.TournamentName,
                tournament.Game,
                tournament.StartDate,
                tournament.EndDate,
                TournamentStatus.Completed,
                tournament.TournamentFormat,
                tournament.MaxTeams,
                tournament.PrizePool,
                tournament.Bracket));
    }

    [Fact]
    public void Update_returns_false_when_nothing_changes()
    {
        var tournament = CreateTournament();

        Assert.False(tournament.Update(
            tournament.TournamentName,
            tournament.Game,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Status,
            tournament.TournamentFormat,
            tournament.MaxTeams,
            tournament.PrizePool,
            tournament.Bracket));
    }

    [Fact]
    public void RegisterTeam_and_UnregisterTeam_work_only_in_expected_states()
    {
        var tournament = CreateTournament(TournamentStatus.RegistrationOpen);

        Assert.True(tournament.RegisterTeam());
        Assert.Equal((byte)1, tournament.RegisteredTeamsCount.Value);
        Assert.True(tournament.UnregisterTeam());
        Assert.Equal((byte)0, tournament.RegisteredTeamsCount.Value);

        Assert.False(CreateTournament().RegisterTeam());
        Assert.False(CreateTournament(TournamentStatus.RegistrationOpen).UnregisterTeam());
    }

    [Fact]
    public void RegisterTeam_rejects_full_tournament()
    {
        var tournament = new Tournament(
            new Organizer(new UserName("organizer")),
            new TournamentName("Full Cup"),
            new Game("Chess"),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow.AddDays(11),
            TournamentFormat.SWISS,
            new MaxTeams(2),
            new Money(100),
            new Bracket(Guid.NewGuid()));

        tournament.Update(
            tournament.TournamentName,
            tournament.Game,
            tournament.StartDate,
            tournament.EndDate,
            TournamentStatus.RegistrationOpen,
            tournament.TournamentFormat,
            tournament.MaxTeams,
            tournament.PrizePool,
            tournament.Bracket);

        Assert.True(tournament.RegisterTeam());
        Assert.True(tournament.RegisterTeam());
        Assert.False(tournament.RegisterTeam());
    }

    [Fact]
    public void Organizer_can_create_edit_and_remove_tournament()
    {
        var organizer = new Organizer(new UserName("organizer"));
        var tournament = organizer.CreateTournament(
            new TournamentName("Cup"),
            new Game("Chess"),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow.AddDays(11),
            TournamentFormat.ROUND_ROBIN,
            new MaxTeams(8),
            new Money(100),
            new Bracket(Guid.NewGuid()));

        Assert.Contains(tournament, organizer.Tournaments);
        Assert.True(organizer.EditTournament(
            tournament,
            tournament.TournamentName,
            new Game("Dota"),
            tournament.StartDate,
            tournament.EndDate,
            tournament.Status,
            tournament.TournamentFormat,
            tournament.MaxTeams,
            tournament.PrizePool,
            tournament.Bracket));
        Assert.True(organizer.RemoveTournament(tournament));
        Assert.Empty(organizer.Tournaments);
    }

    [Fact]
    public void Organizer_rejects_unknown_tournament_edit()
    {
        var organizer = new Organizer(new UserName("organizer"));
        var another = new Tournament(
            organizer,
            new TournamentName("Cup"),
            new Game("Chess"),
            DateTime.UtcNow.AddDays(10),
            DateTime.UtcNow.AddDays(11),
            TournamentFormat.SWISS,
            new MaxTeams(8),
            new Money(100),
            new Bracket(Guid.NewGuid()));

        Assert.False(organizer.EditTournament(
            another,
            another.TournamentName,
            another.Game,
            another.StartDate,
            another.EndDate,
            another.Status,
            another.TournamentFormat,
            another.MaxTeams,
            another.PrizePool,
            another.Bracket));
    }
}
