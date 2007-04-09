using System;
using System.Text;

using octalforty.Brushie.Diff;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.Test
{
    class Program
    {
        static void Main()
        {
            string source = "The metrics for obfuscation are more\\-or\\-well understood. " +
                "Do you have a game plan to become cross\\-media? Think interactive. True. Really. ";
            string target = "The metrics for clarity are more\\-well understood. " +
                "Do you have a game plan to become peerlessly synergetic across all platforms? " +
                "Think interactive. Really. Astonishing.";

            WordDataProvider sourceDataProvider = new WordDataProvider(source);
            WordDataProvider targetDataProvider = new WordDataProvider(target);

            DiffEngine<string> diffEngine = new DiffEngine<string>(
                sourceDataProvider, targetDataProvider);

            PatchOperationCollection differences = diffEngine.GetPatchOperations();

            StringBuilder text = new StringBuilder();

            foreach(PatchOperation difference in differences)
            {
                switch(difference.Type)
                {
                    case PatchOperationType.Deletion:
                        Range<int> deletionRange = GetRange(difference, sourceDataProvider);
                        text.AppendFormat(" -{{background:#FC2F2F;}}{0}- ", source.Substring(deletionRange.Start,
                            deletionRange.End - deletionRange.Start));
                        break;
                    case PatchOperationType.Addition:
                        Range<int> additionRange = GetRange(difference, targetDataProvider);
                        text.AppendFormat(" +{{background:#B1D58B;}}{0}+ ", target.Substring(additionRange.Start,
                            additionRange.End - additionRange.Start));
                        break;
                    case PatchOperationType.Copy:
                        Range<int> copyRange = GetRange(difference, sourceDataProvider);
                        text.Append(source.Substring(copyRange.Start,
                            copyRange.End - copyRange.Start));
                        break;
                } // swich
            } // foreach

            TextileAuthoringEngine textileAuthoringEngine = 
                new TextileAuthoringEngine(new HtmlTextileAuthoringFormatter());
            string html = textileAuthoringEngine.Author(text.ToString(), AuthoringScope.All);

            Console.WriteLine();
        }

        private static Range<int> GetRange(PatchOperation difference, WordDataProvider dataProvider)
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
