using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentService.ValueObjects.Base;
namespace TournamentService.ValueObjects.Validators;

public class BracketIdValidator:IValidator<Guid>
{
    public static bool IsValid(Guid value) => value != Guid.Empty;
    public void Validate(Guid value)
    {
        if (!IsValid(value))
            throw new ArgumentException("BracketId cannot be empty.", nameof(value));
    }
}