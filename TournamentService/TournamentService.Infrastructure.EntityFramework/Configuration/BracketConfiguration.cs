using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentService.Domain;

namespace TournamentService.Infrastructure.EntityFramework.Configuration;

public class BracketConfiguration : IEntityTypeConfiguration<Bracket>
{
    public void Configure(EntityTypeBuilder<Bracket> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
    }
}
