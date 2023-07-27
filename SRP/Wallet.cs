using SRP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRP
{
    public class Wallet
    {
        private ReportService reportService;
        private WalletService walletService;

        private List<Currency> currency = new();

        public string Name { get; }

        public IEnumerable<Currency> Money => currency;
        public IEnumerable<Currency> Coins => CurrencyService.GetCoins(currency);
        public IEnumerable<Currency> Banknotes => CurrencyService.GetBankNotes(currency);
        
        public Wallet(string name)
        {
            Name = name;
            reportService = new ReportService();
            walletService = new WalletService(new ValidateService());
        }

        public void PutMoney(IEnumerable<Currency> money)
        {
            walletService.PutMoney(money, ref currency);
        }

        public void TakeMoney(IEnumerable<Currency> money)
        {
            walletService.TakeMoney(money, ref currency);
        }

        public bool HasMoney(IEnumerable<Currency> money)
        {
           return walletService.HasMoney(money, ref currency);
        }

        public double Total() => CurrencyService.GetTotal(currency);
        
        public string GetFullReport()
        {
            return reportService.GetFullReport(Name, currency);
        }

        public string GetShortReport()
        {
            return reportService.GetShortReport(Name, currency);
        }

      

        
    }
}
