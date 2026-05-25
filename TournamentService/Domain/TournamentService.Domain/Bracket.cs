using TournamentService.Domain.Base;

namespace TournamentService.Domain;

public class Bracket : Entity<Guid>
{
    protected Bracket() : base()
    {
    }
    public Bracket(Guid id) : base(id)
    {
    }
}
