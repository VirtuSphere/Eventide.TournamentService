using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentService.ValueObjects.Base;
using TournamentService.ValueObjects.Validators;

namespace TournamentService.ValueObjects;

public class Money : ValueObject<decimal>
{
    public Money(decimal value)
        : base(new MoneyValidator(), Round(value)) { }

    /// <summary>
    /// Окргуляет до двух знаков после запятой
    /// </summary>
    /// <param name="value">Значение количества денег</param>
    /// <returns></returns>
    public static decimal Round(decimal value)
        => Math.Round(value, 2, MidpointRounding.ToPositiveInfinity);

    public static Money operator +(Money left, Money right)
        => new(left.Value + right.Value);

    public static Money operator -(Money left, Money right)
        => new(left.Value - right.Value);
    public static bool operator <(Money left, Money right)
        => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
        => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
        => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
        => left.Value >= right.Value;
}
