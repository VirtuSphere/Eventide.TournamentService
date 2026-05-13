using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentService.Domain;
using TournamentService.Domain.Enumes;
using TournamentService.ValueObjects;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.Infrastructure.EntityFramework.Configuration;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.TournamentName)
            .IsRequired()
            .HasConversion(value => value.Value, value => new TournamentName(value))
            .HasMaxLength(TournamentNameValidator.MAX_LENGTH);

        builder.Property(x => x.Game)
            .IsRequired()
            .HasConversion(value => value.Value, value => new Game(value))
            .HasMaxLength(GameValidator.MAX_LENGTH);

        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasConversion(
                value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc),
                value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc));

        builder.Property(x => x.EndDate)
            .IsRequired()
            .HasConversion(
                value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc),
                value => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc));

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TournamentFormat)
            .IsRequired();

        builder.Property(x => x.MaxTeams)
            .IsRequired()
            .HasConversion(value => value.Value, value => new MaxTeams(value));

        builder.Property(x => x.PrizePool)
            .IsRequired()
            .HasConversion(value => value.Value, value => new Money(value));

        builder.Property(x => x.RegisteredTeamsCount)
            .IsRequired()
            .HasConversion(value => value.Value, value => new RegisteredTeamsCount(value));

        builder.HasOne(x => x.Bracket)
            .WithMany()
            .HasForeignKey("BracketId")
            .IsRequired();
    }
}
