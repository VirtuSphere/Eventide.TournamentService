using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.ValueObjects;

public sealed class RegisteredTeamsCount : ValueObject<byte>
{
    public RegisteredTeamsCount(byte value)
        : base(new RegisteredTeamsCountValidator(), value)
    {
    }

    public RegisteredTeamsCount Increment()
    {
        if (Value == RegisteredTeamsCountValidator.MAX_VALUE)
        {
            throw new InvalidOperationException($"Registered teams count cannot exceed {RegisteredTeamsCountValidator.MAX_VALUE}.");
        }

        return new RegisteredTeamsCount((byte)(Value + 1));
    }

    public RegisteredTeamsCount Decrement()
    {
        if (Value == 0)
        {
            throw new InvalidOperationException("Registered teams count cannot be less than 0.");
        }

        return new RegisteredTeamsCount((byte)(Value - 1));
    }
}