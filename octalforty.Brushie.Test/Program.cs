using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.Test
{
    class Program
    {
        class Descriptor
        {
            private int index;
            private string value;

            public int Index
            {
                get { return index; }
                set { index = value; }
            }

            public string Value
            {
                get { return value; }
            }

            public Descriptor(int index, string value)
            {
                this.index = index;
                this.value = value;
            }
        }

        static void Main()
        {
            string source = "The metrics for obfuscation are more-or-well understood. " +
                "Do you have a game plan to become cross-media? Think interactive. True. Really. ";
            string target = "The metrics for clarity are more-well understood. " +
                "Do you have a game plan to become peerlessly synergetic across all platforms? " +
                "Think interactive. Really. Astonishing.";

            WordDataProvider sourceDataProvider = new WordDataProvider(source);
            WordDataProvider targetDataProvider = new WordDataProvider(target);

            DiffEngine<string> diffEngine = new DiffEngine<string>(
                sourceDataProvider, targetDataProvider);

            DifferenceCollection differences = diffEngine.GetDifferences();

            foreach(Difference difference in differences)
            {
                switch(difference.Type)
                {
                    case DifferenceType.Deletion:
                        Range<int> deletionRange = GetRange(difference, sourceDataProvider);
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write("-");
                        Console.Write(source.Substring(deletionRange.Start, 
                            deletionRange.End  - deletionRange.Start));
                        Console.Write("-");
                        break;
                    case DifferenceType.Addition:
                        Range<int> additionRange = GetRange(difference, targetDataProvider);
                        Console.ForegroundColor = ConsoleColor.Green;
                        
                        Console.Write("+");
                        Console.Write(target.Substring(additionRange.Start,
                            additionRange.End - additionRange.Start));
                        Console.Write("+");
                        break;
                    case DifferenceType.Copy:
                        Range<int> copyRange = GetRange(difference, sourceDataProvider);
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write(source.Substring(copyRange.Start,
                            copyRange.End - copyRange.Start));
                        break;
                } // swich
            } // foreach

            Console.WriteLine();
        }

        private static Range<int> GetRange(Difference difference, WordDataProvider dataProvider)
        {
            int start = dataProvider.Matches[difference.Range.Start].Index;
            int end = 0;

            for(int match = difference.Range.Start; match <= difference.Range.End; ++match)
                end = Math.Max(end, 
                    dataProvider.Matches[match].Index + dataProvider.Matches[match].Length);

            return new Range<int>(start, end);
        }

        private static Range<int>[] GetChangeRanges(string source, 
            WordDataProvider dataProvider, DifferenceType differenceType, 
            DifferenceCollection differences)
        {
            List<Range<int>> changeRanges = new List<Range<int>>();

            foreach(Difference difference in differences)
            {
                if(difference.Type != differenceType)
                    continue;

                Range<int> range = difference.Type == DifferenceType.Addition ?
                    difference.Addition : difference.Deletion;
                changeRanges.Add(new Range<int>(dataProvider.Matches[range.Start].Index,
                    dataProvider.Matches[range.End].Index + dataProvider.Matches[range.End].Length));
            } // foreach

            return changeRanges.ToArray();
        }

        static Dictionary<string, Descriptor> GetSimilarities(DifferenceCollection differences,
            DifferenceType expectedType, WordDataProvider dataProvider, string source)
        {
            int start = 0;
            int end = 0;

            Dictionary<string, Descriptor> similars = new Dictionary<string, Descriptor>();

            foreach(Difference difference in differences)
            {
                if(difference.Type != expectedType)
                    continue;

                end = dataProvider.Matches[difference.Type == DifferenceType.Addition ?
                    difference.Addition.Start : difference.Deletion.Start].Index;

                string substring = source.Substring(start, end - start);

                Descriptor descriptor = new Descriptor(end, substring);
                similars.Add(substring, descriptor);

                int matchIndexStart = difference.Type == DifferenceType.Addition ?
                    difference.Addition.Start : difference.Deletion.Start;
                int matchIndexEnd = difference.Type == DifferenceType.Addition ?
                    difference.Addition.End : difference.Deletion.End;

                for(int matchIndex = matchIndexStart; matchIndex <= matchIndexEnd; ++matchIndex)
                {
                    Match match = dataProvider.Matches[matchIndex];
                    start = match.Index + match.Length;
                } // for
            } // foreach

            //
            // And the final part
            if(start < source.Length)
            {
                string substring = source.Substring(start, source.Length - start);
                similars.Add(substring, new Descriptor(start, substring));
            } // if

            return similars;
        }
    }
}
