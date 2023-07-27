using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP.Services
{
    public class ValidateService
    {
        public void ValidateMissedMoney(List<Currency> missedMoney)
        {
            if (missedMoney.Count > 0)
            {
                throw new OutOfMoneyException(missedMoney);
            }
        }

        public void ValidateWallet(IEnumerable<Currency> money, List<Currency> currency)
        {
            if (!currency.Any())
            {
                throw new OutOfMoneyException(money);
            }
        }

        public bool IsInValidMoney(IEnumerable<Currency> money)
        {
            if (money == null || !money.Any())
            {
                return true;
            }
            return false;
        }
    }
}
