using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP.Services
{
    public static class CurrencyService
    {
        public static IEnumerable<Currency> GetCoins(List<Currency> currency)
        {
            return currency.Where(m => (int)m <= 200);
        }

        public static IEnumerable<Currency> GetBankNotes(List<Currency> currency)
        {
            return currency.Where(m => (int)m > 200);
        }

        public static double GetTotal(List<Currency> currency)
        {

            return currency.Sum(m => ((double)m) / 100);

        }
    }
}
