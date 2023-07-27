using System;
using System.Collections.Generic;

namespace SRP
{
    public class OutOfMoneyException : Exception
    {
        public IEnumerable<Currency> MissedMoney { get; }

        public OutOfMoneyException(IEnumerable<Currency> missedMoney)
            : base($"Missed money: {String.Join(", ", missedMoney)}")
        {
            MissedMoney = missedMoney;
        }
    }
}
