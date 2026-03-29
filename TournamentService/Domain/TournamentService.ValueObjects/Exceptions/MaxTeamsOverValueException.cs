using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentService.ValueObjects.Exceptions;

internal class MaxTeamsOverValueException(byte value , byte maxvalue)
    : ArgumentException(
        $"The count Teams value cannot be less than {maxvalue}, the passed value is: {value}");