using Eventide.TournamentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Eventide.TournamentService.Infrastructure.Data;

public class TournamentDbContext : DbContext
{
    public DbSet<Tournament> Tournaments => Set<Tournament>();

    public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tournament>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).IsRequired(false).HasMaxLength(500);
            builder.Property(t => t.Game).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Format).HasConversion<string>().IsRequired();
            builder.Property(t => t.Status).HasConversion<string>().IsRequired();
            builder.HasIndex(t => t.OrganizerId);
            
            builder.Property(t => t.ParticipantIds)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null) ?? new List<Guid>()
                );
        });
    }
}