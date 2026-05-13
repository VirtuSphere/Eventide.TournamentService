using Microsoft.EntityFrameworkCore;
using TournamentService.Domain.Repositories.Abstractions.Repositories;
using TournamentService.Domain;
using TournamentService.ValueObjects;

namespace TournamentService.Infrastructure.EntityFramework.RepositoriesEF
{
    public class OrganizerRepository(ApplicationDbContext context) : EfRepository<Organizer, Guid>(context), IOrganizerRepository
    {
        public async Task<Organizer?> GetOrganizerByUsernameAsync(UserName username, CancellationToken cancellationToken)
            => await context.Set<Organizer>()
                .AsNoTracking()
                .FirstOrDefaultAsync(organizer => organizer.UserName == username, cancellationToken);
    }
}
