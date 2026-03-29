using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentService.ValueObjects.Exceptions;

internal class MoneyNegativeValueException(decimal value)
    : ArgumentException(
        $"The money value cannot be less than 0, the passed value is: {value}");