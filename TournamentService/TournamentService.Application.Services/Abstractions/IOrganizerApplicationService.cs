using TournamentService.Application.Models.Organizer;
using TournamentService.Application.Services.Abstractions.Base;

namespace TournamentService.Application.Services.Abstractions;

public interface IOrganizerApplicationService : IApplicationService<OrganizerModel, CreateOrganizerModel, Guid>
{
    Task<OrganizerModel?> GetOrganizerByUsernameAsync(string userName, CancellationToken cancellationToken = default);
}