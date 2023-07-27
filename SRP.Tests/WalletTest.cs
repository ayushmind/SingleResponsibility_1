using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SRP.Tests
{
    [TestClass]
    public class WalletTest
    {
        [TestMethod]
        public void ShouldHasCorrectBanknotesCoinsTotalCount_WhenPutMoney()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles, Currency.TwoKopecks,
                Currency.TenKopecks, Currency.OneHundredRubles
            };
            myWallet.PutMoney(someMoney);

            Assert.AreEqual(someMoney.Count, myWallet.Money.Count());
            Assert.AreEqual(2, myWallet.Banknotes.Count());
            Assert.AreEqual(5, myWallet.Coins.Count());
            Assert.AreEqual(124.12, myWallet.Total());
        }

        [TestMethod]
        public void ShouldHasMoney_WhenCheckedListOfMoneyEqualsPlaced()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles, Currency.TwoKopecks,
                Currency.TenKopecks, Currency.OneHundredRubles
            };
            myWallet.PutMoney(someMoney);

            Assert.IsTrue(myWallet.HasMoney(someMoney));
        }

        [TestMethod]
        public void ShouldHasMoney_ForEachExpectedMoneyItem()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles, Currency.TwoKopecks,
                Currency.TenKopecks, Currency.OneHundredRubles
            };
            myWallet.PutMoney(someMoney);

            foreach (var item in someMoney)
                Assert.IsTrue(myWallet.HasMoney(new List<Currency> {item}));
        }

        [TestMethod]
        public void ShouldNotHasMoney_WhenCheckMissedItems()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles, Currency.TwoKopecks,
                Currency.TenKopecks, Currency.OneHundredRubles
            };
            myWallet.PutMoney(someMoney);

            someMoney.Add(Currency.OneRuble);

            Assert.IsFalse(myWallet.HasMoney(someMoney));
        }

        [TestMethod]
        public void ShouldTakeMoney_WhenMoneyAvailable()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles,
                Currency.FiftyKopecks, Currency.TwoKopecks
            };
            var dept = new List<Currency> {Currency.TwentyRubles, Currency.OneRuble, Currency.TwoKopecks};
            var expectedTotal = new List<Currency> {Currency.OneRuble, Currency.TwoRubles, Currency.FiftyKopecks};

            myWallet.PutMoney(someMoney);

            Assert.AreEqual(24.52, myWallet.Total());
            myWallet.TakeMoney(dept);
            Assert.AreEqual(3.50, myWallet.Total());
            Assert.AreEqual(3, myWallet.Coins.Count());
            Assert.AreEqual(0, myWallet.Banknotes.Count());
            CollectionAssert.AreEqual(expectedTotal, myWallet.Money.ToList());
        }

        [TestMethod]
        public void ShouldThrowOutOfMoneyExceptionWithMissedMoneyList_WhenNotAllRequiredMoneyAvailable()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles,
                Currency.FiftyKopecks, Currency.TwoKopecks
            };
            var dept = new List<Currency>
                {Currency.OneRuble, Currency.TwoKopecks, Currency.TwoKopecks, Currency.FiftyRubles};
            var expectedMissed = new List<Currency> {Currency.TwoKopecks, Currency.FiftyRubles};

            myWallet.PutMoney(someMoney);

            Assert.AreEqual(24.52, myWallet.Total());
            var exception = Assert.ThrowsException<OutOfMoneyException>(() => myWallet.TakeMoney(dept));
            Assert.AreEqual("Missed money: TwoKopecks, FiftyRubles", exception.Message);
            CollectionAssert.AreEqual(expectedMissed, exception.MissedMoney.ToList());
            Assert.AreEqual(24.52, myWallet.Total());
        }

        [TestMethod]
        public void ShouldReturnOnlyExpectedText_WhenShortReport()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles,
                Currency.FiftyKopecks, Currency.TwoKopecks
            };

            myWallet.PutMoney(someMoney);

            var generatedReport = myWallet.GetShortReport();

            Assert.IsNotNull(generatedReport);
            Assert.IsTrue(generatedReport.Contains("Wallet: Hankey"));
            Assert.IsTrue(generatedReport.Contains("Total: 24.52"));
            Assert.IsFalse(generatedReport.Contains("Banknotes:"));
            Assert.IsFalse(generatedReport.Contains("Coins:"));
        }

        [TestMethod]
        public void ShouldReturnFullText_WhenFullReport()
        {
            var myWallet = new Wallet("Hankey");
            var someMoney = new List<Currency>
            {
                Currency.OneRuble, Currency.OneRuble, Currency.TwoRubles, Currency.TwentyRubles,
                Currency.FiftyKopecks, Currency.TwoKopecks
            };

            myWallet.PutMoney(someMoney);

            var generatedReport = myWallet.GetFullReport();

            Assert.IsNotNull(generatedReport);
            Assert.IsTrue(generatedReport.Contains("Wallet: Hankey"));
            Assert.IsTrue(generatedReport.Contains("Total: 24.52"));
            Assert.IsTrue(generatedReport.Contains("Banknotes: TwentyRubles"));
            Assert.IsTrue(generatedReport.Contains("Coins: OneRuble,OneRuble,TwoRubles,FiftyKopecks,TwoKopecks"));
        }
    }
}
