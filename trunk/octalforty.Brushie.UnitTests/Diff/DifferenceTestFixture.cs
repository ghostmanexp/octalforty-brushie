using System;

using NUnit.Framework;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.UnitTests.Diff
{
    /// <summary>
    /// <see cref="Difference"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DifferenceTestFixture
    {
        [Test()]
        public void CreateAddition()
        {
            Difference difference = Difference.CreateAddition(new Range<long>(10, 15));

            Assert.AreEqual(DifferenceType.Addition, difference.Type);
            Assert.AreEqual(new Range<long>(10, 15), difference.Addition);
        }

        [Test()]
        public void CreateDeletion()
        {
            Difference difference = Difference.CreateDeletion(new Range<long>(10, 15));

            Assert.AreEqual(DifferenceType.Deletion, difference.Type);
            Assert.AreEqual(new Range<long>(10, 15), difference.Deletion);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowGetDeletionProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<long>(10, 15));
            Range<long> range = difference.Deletion;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowGetAdditionProperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<long>(10, 15));
            Range<long> range = difference.Addition;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowSetDeletionProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<long>(10, 15));
            difference.Deletion = new Range<long>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowSetAdditionProperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<long>(10, 15));
            difference.Addition = new Range<long>();
        }

        [Test()]
        public void Equals()
        {
            Assert.AreEqual(Difference.CreateAddition(1, 10), Difference.CreateAddition(1, 10));
            Assert.AreEqual(Difference.CreateDeletion(1, 10), Difference.CreateDeletion(1, 10));
            
            Assert.AreNotEqual(Difference.CreateAddition(1, 10), Difference.CreateDeletion(1, 10));
            Assert.AreNotEqual(Difference.CreateAddition(1, 10), Difference.CreateAddition(10, 1));

            Assert.AreNotEqual(Difference.CreateDeletion(1, 10), Difference.CreateAddition(1, 10));
            Assert.AreNotEqual(Difference.CreateDeletion(1, 10), Difference.CreateDeletion(10, 1));

            Assert.AreNotEqual(Difference.CreateAddition(1, 1), null);
            Assert.AreNotEqual(Difference.CreateAddition(1, 1), string.Empty);

            Assert.AreNotEqual(Difference.CreateDeletion(1, 1), null);
            Assert.AreNotEqual(Difference.CreateDeletion(1, 1), string.Empty);
        }
    }
}
