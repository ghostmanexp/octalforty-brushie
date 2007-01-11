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
            Difference difference = Difference.CreateAddition(new Range<int>(10, 15));

            Assert.AreEqual(DifferenceType.Addition, difference.Type);
            Assert.AreEqual(new Range<int>(10, 15), difference.Addition);
        }

        [Test()]
        public void CreateDeletion()
        {
            Difference difference = Difference.CreateDeletion(new Range<int>(10, 15));

            Assert.AreEqual(DifferenceType.Deletion, difference.Type);
            Assert.AreEqual(new Range<int>(10, 15), difference.Deletion);
        }

        [Test()]
        public void CreateCopy()
        {
            Difference difference = Difference.CreateCopy(new Range<int>(10, 15));

            Assert.AreEqual(DifferenceType.Copy, difference.Type);
            Assert.AreEqual(new Range<int>(10, 15), difference.Copy);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowGetDeletionProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<int>(10, 15));
            Range<int> range = difference.Deletion;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowGetCopyProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<int>(10, 15));
            Range<int> range = difference.Copy;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowGetAdditionProperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<int>(10, 15));
            Range<int> range = difference.Addition;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowGetCopyProperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<int>(10, 15));
            Range<int> range = difference.Copy;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowSetDeletionProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<int>(10, 15));
            difference.Deletion = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowSetCopyProperty()
        {
            Difference difference = Difference.CreateAddition(new Range<int>(10, 15));
            difference.Copy = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowSetAdditionProperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<int>(10, 15));
            difference.Addition = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowSetCopyroperty()
        {
            Difference difference = Difference.CreateDeletion(new Range<int>(10, 15));
            difference.Copy = new Range<int>();
        }

        [Test()]
        public void Equals()
        {
            Assert.AreEqual(Difference.CreateAddition(1, 10), Difference.CreateAddition(1, 10));
            Assert.AreEqual(Difference.CreateDeletion(1, 10), Difference.CreateDeletion(1, 10));
            Assert.AreEqual(Difference.CreateCopy(1, 10), Difference.CreateCopy(1, 10));
            
            Assert.AreNotEqual(Difference.CreateAddition(1, 10), Difference.CreateDeletion(1, 10));
            Assert.AreNotEqual(Difference.CreateAddition(1, 10), Difference.CreateCopy(1, 10));
            Assert.AreNotEqual(Difference.CreateAddition(1, 10), Difference.CreateAddition(10, 1));

            Assert.AreNotEqual(Difference.CreateDeletion(1, 10), Difference.CreateAddition(1, 10));
            Assert.AreNotEqual(Difference.CreateDeletion(1, 10), Difference.CreateDeletion(10, 1));
            Assert.AreNotEqual(Difference.CreateDeletion(1, 10), Difference.CreateCopy(10, 1));

            Assert.AreNotEqual(Difference.CreateCopy(1, 10), Difference.CreateAddition(1, 10));
            Assert.AreNotEqual(Difference.CreateCopy(1, 10), Difference.CreateCopy(10, 1));
            Assert.AreNotEqual(Difference.CreateCopy(1, 10), Difference.CreateDeletion(10, 1));

            Assert.AreNotEqual(Difference.CreateAddition(1, 1), null);
            Assert.AreNotEqual(Difference.CreateAddition(1, 1), string.Empty);

            Assert.AreNotEqual(Difference.CreateDeletion(1, 1), null);
            Assert.AreNotEqual(Difference.CreateDeletion(1, 1), string.Empty);

            Assert.AreNotEqual(Difference.CreateCopy(1, 1), null);
            Assert.AreNotEqual(Difference.CreateCopy(1, 1), string.Empty);
        }
    }
}
