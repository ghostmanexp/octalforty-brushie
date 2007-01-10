using NUnit.Framework;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.UnitTests.Diff
{
    /// <summary>
    /// <see cref="Range{TKey}"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class RangeTestFixture
    {
        [Test()]
        public void NoArgumentConstructor()
        {
            Range<int> intRange = new Range<int>();

            Assert.AreEqual(0, intRange.Start);
            Assert.AreEqual(0, intRange.End);

            Range<string> stringRange = new Range<string>();

            Assert.AreEqual(null, stringRange.Start);
            Assert.AreEqual(null, stringRange.End);
        }

        [Test()]
        public void TTConstructor()
        {
            Range<int> intRange = new Range<int>(10, 100);

            Assert.AreEqual(10, intRange.Start);
            Assert.AreEqual(100, intRange.End);

            Range<string> stringRange = new Range<string>("ABC", "DEF");

            Assert.AreEqual("ABC", stringRange.Start);
            Assert.AreEqual("DEF", stringRange.End);
        }

        [Test()]
        public void Equals()
        {
            Range<int> intRange = new Range<int>();

            Assert.IsTrue(intRange.Equals(new Range<int>()));
            Assert.IsTrue(intRange.Equals(new Range<int>(0, 0)));

            Assert.IsFalse(intRange.Equals(new Range<long>(0, 0)));

            Assert.IsFalse(intRange.Equals(null));

            Assert.IsFalse(intRange.Equals(string.Empty));
        }
    }
}
