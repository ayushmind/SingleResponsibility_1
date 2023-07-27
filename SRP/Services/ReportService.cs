using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SRP.Services
{
    public class ReportService
    {
        private const string ReportSeparatorLine = "-----------------------";

        public string GetFullReport(string name, List<Currency> currency)
        {
            StringBuilder result = new StringBuilder();

            SetReportSeparator(ref result);
            SetReportParameters(name, currency, ref result);
            SetAdditionalReportParameters(currency, ref result);
            SetReportSeparator(ref result);

            return result.ToString();
        }

        public string GetShortReport(string name, List<Currency> currency)
        {
            StringBuilder result = new StringBuilder();

            SetReportSeparator(ref result);
            SetReportParameters(name, currency, ref result);
            SetReportSeparator(ref result);

            return result.ToString();
        }

        private void SetReportSeparator(ref StringBuilder result)
        {
            result.AppendLine(ReportSeparatorLine);
        }

        private void SetReportParameters(string name, List<Currency> currency, ref StringBuilder result)
        {
            result.AppendLine($"Wallet: {name}");
            result.AppendLine($"Date: {DateTime.Now.ToLongDateString()}");
            result.AppendLine($"Total: {CurrencyService.GetTotal(currency)}");
        }

        private void SetAdditionalReportParameters(List<Currency> currency, ref StringBuilder result)
        {
            result.AppendLine($"Banknotes: {string.Join(",", CurrencyService.GetBankNotes(currency))}");
            result.AppendLine($"Coins: {string.Join(",", CurrencyService.GetCoins(currency))}");
        }
    }
}
