using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.ValueObjects;

public class BracketId: ValueObject<Guid> {
    public BracketId(Guid value) : base(new BracketIdValidator(), value) { }
}