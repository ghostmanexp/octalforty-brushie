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
            string source = "The metrics for obfuscation are more-or-well understood. Do you have a game plan to become cross-media? Think interactive.";
            string target = "The metrics for clarity are more-well understood. Do you have a game plan to become peerlessly synergetic across all platforms? Think interactive.";

            WordDataProvider sourceDataProvider = new WordDataProvider(source);
            WordDataProvider targetDataProvider = new WordDataProvider(target);

            DiffEngine<string> diffEngine = new DiffEngine<string>(
                sourceDataProvider, targetDataProvider);

            DifferenceCollection differences = diffEngine.GetDifferences();

            //
            // Extracting the similar part which is basically a source string
            // with some words removed.
            List<Descriptor> similars = new List<Descriptor>();

            int start = 0;
            int end = 0;

            foreach(Difference difference in differences)
            {
                if(difference.Type != DifferenceType.Deletion)
                    continue;

                end = sourceDataProvider.Matches[(int)difference.Deletion.Start].Index;

                similars.Add(new Descriptor(end, source.Substring(start, end - start))); 

                for(int matchIndex = (int)difference.Deletion.Start; matchIndex <= difference.Deletion.End;
                    ++matchIndex)
                {
                    Match match = sourceDataProvider.Matches[matchIndex];

                    start = match.Index + match.Length;
                } // for
            } // foreach

            //
            // And the final part
            if(start < source.Length)
                similars.Add(new Descriptor(start, source.Substring(start, source.Length - start)));

            int endIndex = 0;
            foreach(Descriptor descriptor in similars)
            {
                //
                // Preceding it with what's been deleted.
                foreach(Difference difference in differences)
                {
                    if(difference.Type == DifferenceType.Deletion)
                    {
                        Match match = sourceDataProvider.Matches[(int)difference.Deletion.Start];

                        if(match.Index < descriptor.Index && match.Index >= endIndex)
                        {
                            Console.Write("-{0}-", match.Value);
                            break;
                        } // if
                    } // if
                } // foreach

                //
                // Now with added stuff
                foreach(Difference difference in differences)
                {
                    if(difference.Type == DifferenceType.Addition)
                    {
                        Match match = targetDataProvider.Matches[(int)difference.Addition.Start];

                        if(match.Index < descriptor.Index && match.Index >= endIndex)
                        {
                            Console.Write("+{0}+", match.Value);
                            break;
                        } // if
                    } // if
                } // foreach

                //
                // Now the common text
                Console.Write(descriptor.Value);
                endIndex += descriptor.Value.Length;
            } // foreach
        }
    }
}
