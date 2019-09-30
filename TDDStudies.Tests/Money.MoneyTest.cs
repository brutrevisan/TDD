using NUnit.Framework;

namespace TDDStudies.Tests
{
    public class MoneyTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDollarMultiplication()
        {
            Money five = Money.Dollar(5);

            Assert.AreEqual(Money.Dollar(10), five.Times(2));
            Assert.AreEqual(Money.Dollar(15), five.Times(3));
        }

        [Test]
        public void TestFrancMultiplication()
        {
            Money five = Money.Franc(5);
            Assert.AreEqual(Money.Franc(10), five.Times(2));
            Assert.AreEqual(Money.Franc(15), five.Times(3));
        }

        [Test]
        public void TestSimpleAddition()
        {
            Money five = Money.Dollar(5);
            IExpression sum = five.Plus(five);

            Bank bank = new Bank();

            Money reduced = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(10), reduced);
        }

        [Test]
        public void TestMixedAddition()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrancs = Money.Franc(10);

            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            Money result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD");

            Assert.AreEqual(Money.Dollar(10), result);
        }

        [Test]
        public void TestSumPlusMoney()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrancs = Money.Franc(10);

            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            IExpression sum = new Sum(fiveBucks, tenFrancs).Plus(fiveBucks);
            Money result = bank.Reduce(sum, "USD");

            Assert.AreEqual(Money.Dollar(15), result);
        }

        [Test]
        public void TestSumTimes()
        {
            IExpression fiveBucks = Money.Dollar(5);
            IExpression tenFrancs = Money.Franc(10);

            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            IExpression sum = new Sum(fiveBucks, tenFrancs).Times(2);
            Money result = bank.Reduce(sum, "USD");

            Assert.AreEqual(Money.Dollar(20), result);
        }

        [Test]
        public void TestIdentityRate()
        {
            Assert.AreEqual(1, new Bank().Rate("USD", "USD"));
        }

        [Test]
        public void TestReduceMoney()
        {
            Bank bank = new Bank();
            Money result = bank.Reduce(Money.Dollar(1), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [Test]
        public void TestReduceSum()
        {
            IExpression sum = new Sum(Money.Dollar(3), Money.Dollar(4));
            Bank bank = new Bank();

            Money result = bank.Reduce(sum, "USD");
            Assert.AreEqual(Money.Dollar(7), result);
        }

        [Test]
        public void TestPlusReturnSum()
        {
            Money five = Money.Dollar(5);
            IExpression result = five.Plus(five);

            Sum sum = (Sum)result;

            Assert.AreEqual(five, sum.Augend);
            Assert.AreEqual(five, sum.Addend);
        }

        [Test]
        public void TestReduceMoneyDifferentCurrency()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            Money result = bank.Reduce(Money.Franc(2), "USD");
            Assert.AreEqual(Money.Dollar(1), result);
        }

        [Test]
        public void TestEquality()
        {
            Assert.IsTrue(Money.Dollar(5).Equals(Money.Dollar(5)));
            Assert.IsFalse(Money.Dollar(5).Equals(Money.Dollar(6)));
            Assert.IsFalse(Money.Franc(5).Equals(Money.Dollar(5)));
        }

        [Test]
        public void TestCurrency()
        {
            Assert.AreEqual("USD", Money.Dollar(1).Currency);
            Assert.AreEqual("CHF", Money.Franc(1).Currency);
        }

        [Test]
        public void TestDuplicateInsertionInRateHashtable()
        {
            Bank bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
            bank.AddRate("CHF", "USD", 2);
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("10USD", Money.Dollar(10).ToString());
            Assert.AreEqual("10CHF", Money.Franc(10).ToString());
        }
    }
}
