
namespace TournamentService.ValueObjects.Exceptions;

internal class MaxTeamsNegativeValueException(byte value, byte minvalue)
    : ArgumentException(
        $"The count Teams value cannot be less than {minvalue}, the passed value is: {value}");