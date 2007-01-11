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
        public void GetDifferences()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] {"a", "b", "c", "e", "h", "j", "l", "m", "n", "p"},
                    new string[] {"b", "c", "d", "e", "f", "j", "k", "l", "m", "r", "s", "t"});

            Difference[] expectedDifferences = 
                new Difference[] { 
                    Difference.CreateDeletion(0, 0),
                    Difference.CreateCopy(1, 2),
                    Difference.CreateAddition(2, 2),
                    Difference.CreateCopy(3, 3),
                    Difference.CreateAddition(4, 4),
                    Difference.CreateDeletion(4, 4),
                    Difference.CreateCopy(5, 5),
                    Difference.CreateAddition(6, 6),
                    Difference.CreateCopy(6, 7),
                    Difference.CreateAddition(9, 11),
                    Difference.CreateDeletion(8, 9) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences2()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] {"a", "b", "c", "d"},
                    new string[] {"c", "d"});

            Difference[] expectedDifferences = 
                new Difference[] { Difference.CreateDeletion(0, 1)};

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences3()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "x", "y", "z" },
                    new string[] { "c", "d" });

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateDeletion(0, 1),
                    Difference.CreateCopy(2, 3),
                    Difference.CreateDeletion(4, 6) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences4()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "e" },
                    new string[] { "a", "x", "y", "b", "c", "j", "e", });

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateCopy(0, 0),
                    Difference.CreateAddition(1, 2),
                    Difference.CreateCopy(1, 2),
                    Difference.CreateAddition(5, 5),
                    Difference.CreateDeletion(3, 3) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences5()
        {
            DiffEngine<long> diffEngine = new DiffEngine<long>(
                new long[] { 1, 2, 3 }, new long[] { 2, 3 } );

            Difference[] expectedDifferences =
                new Difference[] { Difference.CreateDeletion(0, 0) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences6()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" },
                    new string[] { 
                        "a", "b", "p", "q", "r", "s", "t", "c", "d", "e", "f", "g", "h",
                        "i", "j", "u", "l" });

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateCopy(0, 1),
                    Difference.CreateAddition(2, 6),
                    Difference.CreateCopy(2, 9),
                    Difference.CreateAddition(15, 15),
                    Difference.CreateDeletion(10, 10) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences7()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { 
                        "a", "a", "a", "a", "b", "b", "b", "a", "a", "a", "a", "b", "b", "b", "a", "a",
                        "a", "a", "b", "b", "b", "a", "a", "a", "a", "b", "b", "b" },
                    new string[] { 
                        "a", "a", "a", "a", "b", "b", "b", "a", "b", "b", "b", "a", "a", "a", "a" });

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateCopy(0, 7),
                    Difference.CreateDeletion(8, 10),
                    Difference.CreateCopy(11, 17),
                    Difference.CreateDeletion(18, 27) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences8()
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

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateCopy(0, 2),
                    Difference.CreateAddition(3, 10), 
                    Difference.CreateCopy(3, 87),
                    Difference.CreateAddition(96, 96) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences9()
        {
            DiffEngine<string> diffEngine =
                new DiffEngine<string>(
                    new string[] { "same", "same", "same", "", "same", "del", "", "del" },
                    new string[] { "ins", "", "same", "same", "same", "", "same" });

            Difference[] expectedDifferences =
                new Difference[] {
                    Difference.CreateAddition(0, 1),
                    Difference.CreateCopy(0, 4),
                    Difference.CreateDeletion(5, 7) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        [Test()]
        public void GetDifferences10()
        {
            DiffEngine<string> diffEngine = 
                new DiffEngine<string>(
                    //0    1       2    3         4     5         6         7  8   9   10 11  12   13  14         15     16  17   18
                    "The metrics for obfuscation are more-well understood. Do you have a game plan to become cross-media ? Think interactive".Split(' '),
                    "The metrics for clarity are more-well understood. Do you have a game plan to become peerlessly synergetic across all platforms ? Think interactive".Split(' '));
                    //0    1       2    3     4      5      6           7  8   9   10 11   12  13   14      15        16         17   18   19       20 21     22   

            Difference[] expectedDifferences = 
                new Difference[] {
                    Difference.CreateCopy(0, 2),
                    Difference.CreateAddition(3, 3),
                    Difference.CreateDeletion(3, 3),
                    Difference.CreateCopy(4, 14),
                    Difference.CreateAddition(15, 19),
                    Difference.CreateDeletion(15, 15) };

            AssertDifferences(diffEngine, expectedDifferences);
        }

        private static void AssertDifferences<T>(DiffEngine<T> diffEngine, 
            Difference[] expectedDifferences)
        {
            DifferenceCollection differences = diffEngine.GetDifferences();
            Assert.AreEqual(expectedDifferences.GetLength(0), differences.Count);

            for(int index = 0; index < differences.Count; ++index)
                Assert.AreEqual(differences[index], expectedDifferences[index], 
                    string.Format("Collections differ at index {0}", index));
        }
    }
}
