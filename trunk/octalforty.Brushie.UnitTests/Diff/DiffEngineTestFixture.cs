using NUnit.Framework;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.UnitTests.Diff
{
    /// <summary>
    /// <see cref="DiffEngine{TKey}"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class DiffEngineTestFixture
    {
        [Test()]
        public void GetPatchOperations()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] {"a", "b", "c", "e", "h", "j", "l", "m", "n", "p"},
                    new string[] {"b", "c", "d", "e", "f", "j", "k", "l", "m", "r", "s", "t"});

            PatchOperation[] expectedPatchOperations = 
                new PatchOperation[] { 
                    PatchOperation.CreateDeletion(0, 0),
                    PatchOperation.CreateCopy(1, 2),
                    PatchOperation.CreateAddition(2, 2),
                    PatchOperation.CreateCopy(3, 3),
                    PatchOperation.CreateAddition(4, 4),
                    PatchOperation.CreateDeletion(4, 4),
                    PatchOperation.CreateCopy(5, 5),
                    PatchOperation.CreateAddition(6, 6),
                    PatchOperation.CreateCopy(6, 7),
                    PatchOperation.CreateAddition(9, 11),
                    PatchOperation.CreateDeletion(8, 9) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations2()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] {"a", "b", "c", "d"},
                    new string[] {"c", "d"});

            PatchOperation[] expectedPatchOperations = 
                new PatchOperation[] { PatchOperation.CreateDeletion(0, 1)};

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations3()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "x", "y", "z" },
                    new string[] { "c", "d" });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateDeletion(0, 1),
                    PatchOperation.CreateCopy(2, 3),
                    PatchOperation.CreateDeletion(4, 6) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations4()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "e" },
                    new string[] { "a", "x", "y", "b", "c", "j", "e", });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateCopy(0, 0),
                    PatchOperation.CreateAddition(1, 2),
                    PatchOperation.CreateCopy(1, 2),
                    PatchOperation.CreateAddition(5, 5),
                    PatchOperation.CreateDeletion(3, 3) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations5()
        {
            DiffEngine<long> diffEngine = new DiffEngine<long>(
                new long[] { 1, 2, 3 }, new long[] { 2, 3 } );

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] { PatchOperation.CreateDeletion(0, 0) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations6()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" },
                    new string[] { 
                        "a", "b", "p", "q", "r", "s", "t", "c", "d", "e", "f", "g", "h",
                        "i", "j", "u", "l" });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateCopy(0, 1),
                    PatchOperation.CreateAddition(2, 6),
                    PatchOperation.CreateCopy(2, 9),
                    PatchOperation.CreateAddition(15, 15),
                    PatchOperation.CreateDeletion(10, 10) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations7()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { 
                        "a", "a", "a", "a", "b", "b", "b", "a", "a", "a", "a", "b", "b", "b", "a", "a",
                        "a", "a", "b", "b", "b", "a", "a", "a", "a", "b", "b", "b" },
                    new string[] { 
                        "a", "a", "a", "a", "b", "b", "b", "a", "b", "b", "b", "a", "a", "a", "a" });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateCopy(0, 7),
                    PatchOperation.CreateDeletion(8, 10),
                    PatchOperation.CreateCopy(11, 17),
                    PatchOperation.CreateDeletion(18, 27) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations8()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] {
                        "A", "B", "C", "D", "E", "F", "G", "A", "H", "I", "J", "D", "K",
                        "L", "C", "G", "M", "H", "N", "J", "I", "K", "O", "C", "G", "M",
                        "P", "Q", "J", "R", "K", "S", "C", "C", "F", "G", "D", "TKey", "N",
                        "G", "M", "U", "V", "J", "Q", "K", "W", "C", "G", "M", "X", "C",
                        "V", "K", "Y", "C", "G", "G", "A", "Z", "AA", "J", "C", "Z", "G",
                        "V", "K", "BB", "C", "G", "M", "CC", "DD", "J", "EE", "K", "FF",
                        "C", "AA", "G", "M", "GG", "K", "HH", "C", "DD", "G", "M", "II",
                        "II", "II" },
                    new string[] {
                        "A", "B", "C", "JJ", "G", "A", "II", "KK", "A", "B", "C", "D",
                        "E", "F", "G", "A", "H", "I", "J", "D", "K", "L", "C", "G", "M",
                        "H", "N", "J", "I", "K", "O", "C", "G", "M", "P", "Q", "J", "R",
                        "K", "S", "C", "C", "F", "G", "D", "TKey", "N", "G", "M", "U", "V",
                        "J", "Q", "K", "W", "C", "G", "M", "X", "C", "V", "K", "Y", "C",
                        "G", "G", "A", "Z", "AA", "J", "C", "Z", "G", "V", "K", "BB", "C", 
                        "G", "M", "CC", "DD", "J", "EE", "K", "FF", "C", "AA", "G", "M", 
                        "GG", "K", "HH", "C", "DD", "G", "M", "II", "II", "II", "II" });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateCopy(0, 2),
                    PatchOperation.CreateAddition(3, 10), 
                    PatchOperation.CreateCopy(3, 87),
                    PatchOperation.CreateAddition(96, 96) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations9()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "same", "same", "same", "", "same", "del", "", "del" },
                    new string[] { "ins", "", "same", "same", "same", "", "same" });

            PatchOperation[] expectedPatchOperations =
                new PatchOperation[] {
                    PatchOperation.CreateAddition(0, 1),
                    PatchOperation.CreateCopy(0, 4),
                    PatchOperation.CreateDeletion(5, 7) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        [Test()]
        public void GetPatchOperations10()
        {
            DiffEngine<string> diffEngine = 
                new DiffEngine<string>(
                    //0    1       2    3         4     5         6         7  8   9   10 11  12   13  14         15     16  17   18
                    "The metrics for obfuscation are more-well understood. Do you have a game plan to become cross-media ? Think interactive".Split(' '),
                    "The metrics for clarity are more-well understood. Do you have a game plan to become peerlessly synergetic across all platforms ? Think interactive".Split(' '));
                    //0    1       2    3     4      5      6           7  8   9   10 11   12  13   14      15        16         17   18   19       20 21     22   

            PatchOperation[] expectedPatchOperations = 
                new PatchOperation[] {
                    PatchOperation.CreateCopy(0, 2),
                    PatchOperation.CreateAddition(3, 3),
                    PatchOperation.CreateDeletion(3, 3),
                    PatchOperation.CreateCopy(4, 14),
                    PatchOperation.CreateAddition(15, 19),
                    PatchOperation.CreateDeletion(15, 15) };

            AssertDifferences(diffEngine, expectedPatchOperations);
        }

        private static void AssertDifferences<T>(DiffEngine<T> diffEngine, 
            PatchOperation[] expectedPatchOperations)
        {
            PatchOperationCollection patchOperations = diffEngine.GetPatchOperations();
            Assert.AreEqual(expectedPatchOperations.GetLength(0), patchOperations.Count);

            for(int index = 0; index < patchOperations.Count; ++index)
                Assert.AreEqual(patchOperations[index], expectedPatchOperations[index], 
                    string.Format("Collections differ at index {0}", index));
        }
    }
}
