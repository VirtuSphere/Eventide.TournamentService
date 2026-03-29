using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentService.ValueObjects.Exceptions;

internal class MaxTeamsNegativeValueException(byte value, byte minvalue)
    : ArgumentException(
        $"The count Teams value cannot be less than {minvalue}, the passed value is: {value}");