
using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Exceptions;

namespace TournamentService.ValueObjects.Validators;

public class MoneyValidator : IValidator<decimal>
{
    public static bool IsValid(decimal value) => value >= 0 && value <= 999_999.99m;
    /// <summary>
    /// Verifies the decimal to make sure it is not negative.
    /// </summary>
    /// <param name="value">A decimal containing data.</param>
    /// <exception cref="ArgumentNegativeValueException"></exception>
    public void Validate(decimal value)
    {
        if (value < 0) throw new MoneyNegativeValueException(value);

        if (!IsValid(value)) throw new ValidationInconsistencyException();
    }
}
