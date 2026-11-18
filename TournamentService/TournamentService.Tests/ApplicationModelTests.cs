using TournamentService.Application.Models.Bracket;
using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Models.Tournament;
using TournamentService.Domain.Enumes;

namespace TournamentService.Tests;

public sealed class ApplicationModelTests
{
    [Fact]
    public void Organizer_models_keep_values()
    {
        var create = new CreateOrganizerModel("organizer");
        var model = new OrganizerModel(Guid.NewGuid(), "organizer");

        Assert.Equal("organizer", create.UserName);
        Assert.Equal(model.Id, model.Id);
        Assert.Equal("organizer", model.UserName);
    }

    [Fact]
    public void Bracket_models_keep_id()
    {
        var id = Guid.NewGuid();

        Assert.Equal(id, new CreateBracketModel(id).Id);
        Assert.Equal(id, new BracketModel(id).Id);
    }

    [Fact]
    public void Tournament_create_model_keeps_values()
    {
        var model = new CreateTournamentModel(
            Guid.NewGuid(),
            "Cup",
            "Chess",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            TournamentFormat.SWISS,
            8,
            100,
            Guid.NewGuid());

        Assert.Equal("Cup", model.TournamentName);
        Assert.Equal("Chess", model.Game);
        Assert.Equal(TournamentFormat.SWISS, model.TournamentFormat);
        Assert.Equal((byte)8, model.MaxTeams);
    }

    [Fact]
    public void Tournament_model_keeps_values()
    {
        var model = new TournamentModel(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Cup",
            "Chess",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(1),
            TournamentStatus.Draft,
            TournamentFormat.SINGLE_ELIMINATION,
            8,
            100,
            Guid.NewGuid(),
            0);

        Assert.Equal("Cup", model.TournamentName);
        Assert.Equal(TournamentStatus.Draft, model.Status);
        Assert.Equal((byte)0, model.RegisteredTeamsCount);
    }
}
