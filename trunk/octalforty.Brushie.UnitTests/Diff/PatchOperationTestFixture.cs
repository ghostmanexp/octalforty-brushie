using System;

using NUnit.Framework;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.UnitTests.Diff
{
    /// <summary>
    /// <see cref="PatchOperation"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class PatchOperationTestFixture
    {
        [Test()]
        public void CreateAddition()
        {
            PatchOperation patchOperation = PatchOperation.CreateAddition(new Range<int>(10, 15));

            Assert.AreEqual(PatchOperationType.Addition, patchOperation.Type);
            Assert.AreEqual(new Range<int>(10, 15), patchOperation.Addition);
        }

        [Test()]
        public void CreateDeletion()
        {
            PatchOperation patchOperation = PatchOperation.CreateDeletion(new Range<int>(10, 15));

            Assert.AreEqual(PatchOperationType.Deletion, patchOperation.Type);
            Assert.AreEqual(new Range<int>(10, 15), patchOperation.Deletion);
        }

        [Test()]
        public void CreateCopy()
        {
            PatchOperation patchOperation = PatchOperation.CreateCopy(new Range<int>(10, 15));

            Assert.AreEqual(PatchOperationType.Copy, patchOperation.Type);
            Assert.AreEqual(new Range<int>(10, 15), patchOperation.Copy);
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowGetDeletionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateAddition(new Range<int>(10, 15));
            Range<int> range = patchOperation.Deletion;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowGetCopyProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateAddition(new Range<int>(10, 15));
            Range<int> range = patchOperation.Copy;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowGetAdditionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateDeletion(new Range<int>(10, 15));
            Range<int> range = patchOperation.Addition;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowGetCopyProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateDeletion(new Range<int>(10, 15));
            Range<int> range = patchOperation.Copy;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowSetDeletionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateAddition(new Range<int>(10, 15));
            patchOperation.Deletion = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AdditionDifferenceDoesNotAllowSetCopyProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateAddition(new Range<int>(10, 15));
            patchOperation.Copy = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowSetAdditionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateDeletion(new Range<int>(10, 15));
            patchOperation.Addition = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeletionDifferenceDoesNotAllowSetCopyProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateDeletion(new Range<int>(10, 15));
            patchOperation.Copy = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CopyDifferenceDoesNotAllowSetAdditionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateCopy(new Range<int>(10, 15));
            patchOperation.Addition = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CopyDifferenceDoesNotAllowSetDeletionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateCopy(new Range<int>(10, 15));
            patchOperation.Deletion = new Range<int>();
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CopyDifferenceDoesNotAllowGetAdditionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateCopy(new Range<int>(10, 15));
            Range<int> range = patchOperation.Addition;
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CopyDifferenceDoesNotAllowGetDeletionProperty()
        {
            PatchOperation patchOperation = PatchOperation.CreateCopy(new Range<int>(10, 15));
            Range<int> range = patchOperation.Deletion;
        }

        [Test()]
        public void Equals()
        {
            Assert.AreEqual(PatchOperation.CreateAddition(1, 10), PatchOperation.CreateAddition(1, 10));
            Assert.AreEqual(PatchOperation.CreateDeletion(1, 10), PatchOperation.CreateDeletion(1, 10));
            Assert.AreEqual(PatchOperation.CreateCopy(1, 10), PatchOperation.CreateCopy(1, 10));
            
            Assert.AreNotEqual(PatchOperation.CreateAddition(1, 10), PatchOperation.CreateDeletion(1, 10));
            Assert.AreNotEqual(PatchOperation.CreateAddition(1, 10), PatchOperation.CreateCopy(1, 10));
            Assert.AreNotEqual(PatchOperation.CreateAddition(1, 10), PatchOperation.CreateAddition(10, 1));

            Assert.AreNotEqual(PatchOperation.CreateDeletion(1, 10), PatchOperation.CreateAddition(1, 10));
            Assert.AreNotEqual(PatchOperation.CreateDeletion(1, 10), PatchOperation.CreateDeletion(10, 1));
            Assert.AreNotEqual(PatchOperation.CreateDeletion(1, 10), PatchOperation.CreateCopy(10, 1));

            Assert.AreNotEqual(PatchOperation.CreateCopy(1, 10), PatchOperation.CreateAddition(1, 10));
            Assert.AreNotEqual(PatchOperation.CreateCopy(1, 10), PatchOperation.CreateCopy(10, 1));
            Assert.AreNotEqual(PatchOperation.CreateCopy(1, 10), PatchOperation.CreateDeletion(10, 1));

            Assert.AreNotEqual(PatchOperation.CreateAddition(1, 1), null);
            Assert.AreNotEqual(PatchOperation.CreateAddition(1, 1), string.Empty);

            Assert.AreNotEqual(PatchOperation.CreateDeletion(1, 1), null);
            Assert.AreNotEqual(PatchOperation.CreateDeletion(1, 1), string.Empty);

            Assert.AreNotEqual(PatchOperation.CreateCopy(1, 1), null);
            Assert.AreNotEqual(PatchOperation.CreateCopy(1, 1), string.Empty);
        }
    }
}
