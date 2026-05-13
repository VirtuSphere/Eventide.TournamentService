using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentService.Domain;
using TournamentService.ValueObjects;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.Infrastructure.EntityFramework.Configuration;

public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasConversion(value => value.Value, value => new UserName(value))
            .HasMaxLength(UserNameValidator.MAX_LENGTH);

        builder.Ignore(x => x.Tournaments);

        builder.HasMany<Tournament>("_tournaments")
            .WithOne(x => x.Organizer)
            .HasForeignKey("OrganizerId")
            .IsRequired();
    }
}
