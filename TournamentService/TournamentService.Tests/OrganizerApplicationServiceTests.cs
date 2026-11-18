using AutoMapper;
using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Services;
using TournamentService.Application.Services.Mapping;
using TournamentService.Domain;
using TournamentService.ValueObjects;

namespace TournamentService.Tests;

public sealed class OrganizerApplicationServiceTests
{
    private readonly FakeOrganizerRepository _repository = new();
    private readonly OrganizerApplicationService _service;

    public OrganizerApplicationServiceTests()
    {
        var configuration = new MapperConfiguration(
            options => options.AddProfile<ApplicationProfile>());
        var mapper = configuration.CreateMapper();
        _service = new OrganizerApplicationService(_repository, mapper);
    }

    [Fact]
    public async Task GetModelsAsync_returns_mapped_organizers()
    {
        await _repository.AddAsync(new Organizer(new UserName("first")));

        var result = await _service.GetModelsAsync();

        Assert.Single(result);
        Assert.Equal("first", result.Single().UserName);
    }

    [Fact]
    public async Task GetModelByIdAsync_returns_model_or_null()
    {
        var organizer = new Organizer(new UserName("first"));
        await _repository.AddAsync(organizer);

        var found = await _service.GetModelByIdAsync(organizer.Id);
        var missing = await _service.GetModelByIdAsync(Guid.NewGuid());

        Assert.NotNull(found);
        Assert.Null(missing);
    }

    [Fact]
    public async Task CreateModelAsync_creates_unique_organizer()
    {
        var created = await _service.CreateModelAsync(new CreateOrganizerModel("unique"));

        Assert.NotNull(created);
        Assert.Equal("unique", created.UserName);
        Assert.Single(_repository.Items);

        var duplicate = await _service.CreateModelAsync(new CreateOrganizerModel("unique"));
        Assert.Null(duplicate);
    }

    [Fact]
    public async Task GetOrganizerByUsernameAsync_returns_model_or_null()
    {
        await _repository.AddAsync(new Organizer(new UserName("first")));

        Assert.NotNull(await _service.GetOrganizerByUsernameAsync("first"));
        Assert.Null(await _service.GetOrganizerByUsernameAsync("missing"));
    }

    [Fact]
    public async Task UpdateModelAsync_updates_existing_unique_username()
    {
        var organizer = new Organizer(new UserName("first"));
        await _repository.AddAsync(organizer);

        var updated = await _service.UpdateModelAsync(
            new OrganizerModel(organizer.Id, "updated"));

        Assert.True(updated);
        Assert.Equal("updated", organizer.UserName.Value);
    }

    [Fact]
    public async Task UpdateModelAsync_rejects_missing_or_duplicate_organizer()
    {
        var first = new Organizer(new UserName("first"));
        var second = new Organizer(new UserName("second"));
        await _repository.AddAsync(first);
        await _repository.AddAsync(second);

        Assert.False(await _service.UpdateModelAsync(
            new OrganizerModel(Guid.NewGuid(), "missing")));
        Assert.False(await _service.UpdateModelAsync(
            new OrganizerModel(first.Id, "second")));
    }

    [Fact]
    public async Task DeleteModelAsync_deletes_existing_and_rejects_missing()
    {
        var organizer = new Organizer(new UserName("first"));
        await _repository.AddAsync(organizer);

        Assert.True(await _service.DeleteModelAsync(organizer.Id));
        Assert.False(await _service.DeleteModelAsync(organizer.Id));
    }

    [Fact]
    public void ApplicationProfile_maps_domain_models()
    {
        var configuration = new MapperConfiguration(
            options => options.AddProfile<ApplicationProfile>());
        var mapper = configuration.CreateMapper();

        var organizer = new Organizer(new UserName("first"));
        Assert.Equal("first", mapper.Map<OrganizerModel>(organizer).UserName);
    }
}
