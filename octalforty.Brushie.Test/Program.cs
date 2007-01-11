using System;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.Test
{
    class Program
    {
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
    }
}
