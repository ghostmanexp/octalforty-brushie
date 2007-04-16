using System;
using System.Text;

using octalforty.Brushie.Diff;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Textile;
using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Test
{
    class Program
    {
        #region Wiki Markup
        const String WikiMarkup = "h2(head#h2){color:green}[en-US]<>(). This is a title\r\n\r\n" +
                                      "h3. This is a subhead\r\n\r\n" +
                                      "p{color:red}. This is some text of dubious character. Isn't the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.\r\n\r\n" +
                                      "bq[fr]. This is a block quote. I'll admit it's not the most exciting block quote ever devised.\r\n\r\n" +
                                      "Simple list:\r\n\r\n" +
                                      "#{color:blue} one\r\n" +
                                      "# two\r\n" +
                                      "# three\r\n\r\n" +
                                      "Multi-level list:\r\n\r\n" +
                                      "# one\r\n" +
                                      "## aye\r\n" +
                                      "## bee\r\n" +
                                      "## see\r\n" +
                                      "# two\r\n" +
                                      "## x\r\n" +
                                      "## y\r\n" +
                                      "# three\r\n\r\n" +
                                      "Mixed list:\r\n\r\n" +
                                      "* Point one\r\n" +
                                      "* Point two\r\n" +
                                      "## Step 1\r\n" +
                                      "## Step 2\r\n" +
                                      "## Step 3\r\n" +
                                      "* Point three\r\n" +
                                      "** Sub point 1\r\n" +
                                      "** Sub point 2\r\n\r\n\r\n" +
                                      "Well, that went well[1]. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!" +
                                      "\"This is a link (optional title)\":http://www.textism.com\r\n\r\n" +
            /*"table{border:1px solid black}.\r\n" +
            "|_. this|_. is|_. a|_. header|\r\n" +
            "<{background:gray}. |\\2. this is|{background:red;width:200px}. a|^<>{height:200px}. row|\r\n" +
            "|this|<>{padding:10px}. is|^. another|(bob#bob). row|\r\n\r\n" +*/
                                      "An image:\r\n\r\n" +
                                      "!http://www.google.com/intl/en_ALL/images/logo.gif(Google)!\r\n\r\n" +
                                      "# Librarians rule\r\n" +
                                      "# Yes they do\r\n" +
                                      "# But you knew that\r\n\r\n" +
                                      "Some more text of dubious character. Here is a noisome string of CAPITAL letters. Here is something we want to _emphasize_.\r\n" +
                                      "That was a **[en-US]linebreak**. And something to indicate *strength*. Of course I could use <em>my own HTML tags</em> if I <strong>felt</strong> like it.\r\n\r\n" +
                                      "h3. Coding\r\n\r\n" +
                                      "This <code>is some code, \"isn't it\"</code>. Watch those quote marks! Now for some preformatted text:\r\n\r\n" +
                                      "<pre class=\"test\">\r\n" +
                                      "$text = str_replace(\"<p>%::%</p>\",\"\",$text);\r\n" +
                                      "$text = str_replace(\"%::%</p>\",\"\",$text);\r\n" +
                                      "$text = str_replace(\"%::%\",\"\",$text);\r\n" +
                                      "</pre>\r\n\r\n" +
                                      "This isn't code.\r\n\r\n" +
                                      "So you see, my friends:\r\n\r\n" +
            /*"* The time is now\r\n" +
            "* The time is not later\r\n" +
            "* The time is not yesterday\r\n" +*/
                                      "* We must act\r\n\r\n" +
                                      "fn1. Footnote.\r\n\r\n";
        #endregion

        static void Main()
        {
            TextileParser textileParser = new TextileParser();
            Document document = textileParser.Parse(WikiMarkup);

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
                        text.AppendFormat(" %{{background:#FC2F2F;}}{0}% ", source.Substring(deletionRange.Start,
                            deletionRange.End - deletionRange.Start));
                        break;
                    case PatchOperationType.Addition:
                        Range<int> additionRange = GetRange(difference, targetDataProvider);
                        text.AppendFormat(" %{{background:#B1D58B;}}{0}% ", target.Substring(additionRange.Start,
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
