using Eventide.TournamentService.Domain.Entities;
using Eventide.TournamentService.Domain.Interfaces;
using Eventide.TournamentService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eventide.TournamentService.Infrastructure.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private readonly TournamentDbContext _context;

    public TournamentRepository(TournamentDbContext context) => _context = context;

    public async Task<Tournament?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _context.Tournaments.FindAsync(new object[] { id }, ct);

    public async Task<List<Tournament>> GetUpcomingAsync(int skip, int take, CancellationToken ct)
        => await _context.Tournaments
            .Where(t => t.TournamentStart > DateTime.UtcNow)
            .OrderBy(t => t.TournamentStart)
            .Skip(skip).Take(take).ToListAsync(ct);

    public async Task<List<Tournament>> GetByOrganizerAsync(Guid organizerId, CancellationToken ct)
        => await _context.Tournaments.Where(t => t.OrganizerId == organizerId).ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
        => await _context.Tournaments.AnyAsync(t => t.Name == name, ct);

    public async Task AddAsync(Tournament tournament, CancellationToken ct)
        => await _context.Tournaments.AddAsync(tournament, ct);

    public Task UpdateAsync(Tournament tournament, CancellationToken ct)
    { _context.Tournaments.Update(tournament); return Task.CompletedTask; }

    public async Task SaveChangesAsync(CancellationToken ct) => await _context.SaveChangesAsync(ct);
}