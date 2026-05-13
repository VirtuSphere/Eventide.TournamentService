using TournamentService.ValueObjects.Base;

namespace TournamentService.ValueObjects.Validators;

public sealed class RegisteredTeamsCountValidator : IValidator<byte>
{
    public const byte MIN_VALUE = 0;
    public const byte MAX_VALUE = 100;

    public void Validate(byte value)
    {
        if (value < MIN_VALUE || value > MAX_VALUE)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Registered teams count must be between {MIN_VALUE} and {MAX_VALUE}.");
        }
    }
}