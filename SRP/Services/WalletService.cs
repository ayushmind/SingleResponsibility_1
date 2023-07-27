using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRP.Services
{
    public class WalletService
    {
        private ValidateService validateService;
        public WalletService(ValidateService validateService)
        {
            this.validateService = validateService;
        }
        public void PutMoney(IEnumerable<Currency> money , ref List<Currency> currency)
        {
            currency?.AddRange(money);
        }

        public void TakeMoney(IEnumerable<Currency> money, ref List<Currency> currency)
        {
            if (validateService.IsInValidMoney(money))
            {
                return;
            }

            validateService.ValidateWallet(money, currency);

            currency = TakeMoney(FormatService.ToDictionary(money), FormatService.ToDictionary(currency));
        }
        public bool HasMoney(IEnumerable<Currency> money, ref List<Currency> _currency)
        {
            if (validateService.IsInValidMoney(money))
            {
                return true;
            }

            Dictionary<Currency, int> credit = FormatService.ToDictionary(_currency);

            return !FormatService.ToDictionary(money).Any(entry => !credit.ContainsKey(entry.Key) || credit[entry.Key] < entry.Value);
        }

        private List<Currency> TakeMoney(Dictionary<Currency, int> money, Dictionary<Currency, int> credit)
        {
            List<Currency> missedMoney = new List<Currency>();

            foreach (KeyValuePair<Currency, int> item in money)
            {
                if (credit.ContainsKey(item.Key) && credit[item.Key] >= item.Value)
                {
                    credit[item.Key] -= item.Value;
                }
                else
                {
                    missedMoney.Add(item.Key);
                }
            }

            validateService.ValidateMissedMoney(missedMoney);

            return FormatService.ToList(credit).ToList();
        }

        
    }
}
