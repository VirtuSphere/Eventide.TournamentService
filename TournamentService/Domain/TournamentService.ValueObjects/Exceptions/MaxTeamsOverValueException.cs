
namespace TournamentService.ValueObjects.Exceptions;

internal class MaxTeamsOverValueException(byte value , byte maxvalue)
    : ArgumentException(
        $"The count Teams value cannot be less than {maxvalue}, the passed value is: {value}");