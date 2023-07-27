using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP.Services
{
    public static class FormatService
    {
        public static Dictionary<Currency, int> ToDictionary(IEnumerable<Currency> money)
        {
            return (from item in money
                    group item by item
                    into batch
                    select new { currency = batch.Key, count = batch.Count() })
                .ToDictionary(x => x.currency, x => x.count);
        }

        public static IEnumerable<Currency> ToList(Dictionary<Currency, int> money)
        {
            List<Currency> result = new List<Currency>();
            foreach (KeyValuePair<Currency, int> item in money.Where(i => i.Value > 0))
            {
                result.AddRange(Enumerable.Repeat(item.Key, item.Value));
            }
            return result;
        }
    }
}
