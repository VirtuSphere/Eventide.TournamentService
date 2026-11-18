using AutoMapper;
using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Services.Abstractions;
using TournamentService.Domain;
using TournamentService.Domain.Repositories.Abstractions.Repositories;
using TournamentService.ValueObjects;

namespace TournamentService.Application.Services;

public sealed class OrganizerApplicationService(
    IOrganizerRepository repository,
    IMapper mapper) : IOrganizerApplicationService
{
    public async Task<IEnumerable<OrganizerModel>> GetModelsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken))
            .Select(mapper.Map<OrganizerModel>);

    public async Task<OrganizerModel?> GetModelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var organizer = await repository.GetByIdAsync(id, cancellationToken);
        return organizer is null ? null : mapper.Map<OrganizerModel>(organizer);
    }

    public async Task<OrganizerModel?> GetOrganizerByUsernameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        var organizer = await repository.GetOrganizerByUsernameAsync(new UserName(userName), cancellationToken);
        return organizer is null ? null : mapper.Map<OrganizerModel>(organizer);
    }

    public async Task<OrganizerModel?> CreateModelAsync(
        CreateOrganizerModel organizerInformation,
        CancellationToken cancellationToken = default)
    {
        if (await GetOrganizerByUsernameAsync(organizerInformation.UserName, cancellationToken) is not null)
            return null;

        var organizer = new Organizer(new UserName(organizerInformation.UserName));
        await repository.AddAsync(organizer, cancellationToken);
        return mapper.Map<OrganizerModel>(organizer);
    }

    public async Task<bool> UpdateModelAsync(
        OrganizerModel organizerInformation,
        CancellationToken cancellationToken = default)
    {
        var organizer = await repository.GetByIdAsync(organizerInformation.Id, cancellationToken);
        if (organizer is null)
            return false;

        var existing = await repository.GetOrganizerByUsernameAsync(
            new UserName(organizerInformation.UserName),
            cancellationToken);
        if (existing is not null && existing.Id != organizer.Id)
            return false;

        organizer.ChangeUserName(new UserName(organizerInformation.UserName));
        await repository.UpdateAsync(organizer, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteModelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var organizer = await repository.GetByIdAsync(id, cancellationToken);
        if (organizer is null)
            return false;

        await repository.DeleteAsync(organizer, cancellationToken);
        return true;
    }
}