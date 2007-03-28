using System;

using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="TextileAuthoringEngine"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class TextileAuthoringEngineTestFixture
    {
        #region Private Constants
        private const String WikiMarkup = "h2(head#h2){color:green}[en-US]<>(). This is a title\n\n" +
                                          "h3. This is a subhead\n\n" +
                                          "p{color:red}. This is some text of dubious character. Isn't the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.\n\n" +
                                          "bq[fr]. This is a block quote. I'll admit it's not the most exciting block quote ever devised.\n\n" +
                                          "Simple list:\n\n" +
                                          "#{color:blue} one\n" +
                                          "# two\n" +
                                          "# three\n\n" +
                                          "Multi-level list:\n\n" +
                                          "# one\n" +
                                          "## aye\n" +    
                                          "## bee\n" +
                                          "## see\n" +
                                          "# two\n" +
                                          "## x\n" +
                                          "## y\n" +
                                          "# three\n\n" +
                                          "Mixed list:\n\n" +
                                          "* Point one\n" +
                                          "* Point two\n" +
                                          "## Step 1\n" +
                                          "## Step 2\n" +
                                          "## Step 3\n" +
                                          "* Point three\n" +
                                          "** Sub point 1\n" +
                                          "** Sub point 2\n\n\n" +
                                          "Well, that went well. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!" +
                                          "\"This is a link (optional title)\":http://www.textism.com\n\n" +
                                          /*"table{border:1px solid black}.\n" +
                                          "|_. this|_. is|_. a|_. header|\n" +
                                          "<{background:gray}. |\\2. this is|{background:red;width:200px}. a|^<>{height:200px}. row|\n" +
                                          "|this|<>{padding:10px}. is|^. another|(bob#bob). row|\n\n" +*/
                                          "An image:\n\n" +
                                          "!/common/textist.gif(optional alt text)!\n\n" +
                                          "# Librarians rule\n" +
                                          "# Yes they do\n" +
                                          "# But you knew that\n\n" +
                                          "Some more text of dubious character. Here is a noisome string of CAPITAL letters. Here is something we want to _emphasize_.\n" +
                                          "That was a **[en-US]linebreak**. And something to indicate *strength*. Of course I could use <em>my own HTML tags</em> if I <strong>felt</strong> like it.\n\n" +
                                          "h3. Coding\n\n" +
                                          "This <code>is some code, \"isn't it\"</code>. Watch those quote marks! Now for some preformatted text:\n\n" +
                                          "<pre>\n" +
                                          "<code>\n" +
                                          "$text = str_replace(\"<p>%::%</p>\",\"\",$text);\n" +
                                          "$text = str_replace(\"%::%</p>\",\"\",$text);\n" +
                                          "$text = str_replace(\"%::%\",\"\",$text);\n" +
                                          "</code>\n" +
                                          "</pre>\n\n" +
                                          "This isn't code.\n\n" +
                                          "So you see, my friends:\n\n" +
                                          "* The time is now\n" +
                                          "* The time is not later\n" +
                                          "* The time is not yesterday\n" +
                                          "* We must act\n";
        #endregion

        [Test()]
        public void Author()
        {
            TextileAuthoringEngine authoringEngine = 
                new TextileAuthoringEngine(new HtmlTextileAuthoringFormatter());

            Assert.AreEqual("<h2 class=\"head\" id=\"h2\" lang=\"en-US\" style=\"color:green;text-align: justify;padding-left: 1em;padding-right: 1em;\">This is a title</h2>\n" +
                "<h3>This is a subhead</h3>\n" +
                "p{color:red}. This is some text of dubious character. Isn't the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.\n\n" +
                "<blockquote lang=\"fr\">This is a block quote. I'll admit it's not the most exciting block quote ever devised.</blockquote>\n" +
                "Simple list:\n\n" +
                "#{color:blue} one\n" +
                "# two\n" +
                "# three\n\n" +
                "Multi-level list:\n\n" +
                "# one\n" +
                "## aye\n" +    
                "## bee\n" +
                "## see\n" +
                "# two\n" +
                "## x\n" +
                "## y\n" +
                "# three\n\n" +
                "Mixed list:\n\n" +
                "* Point one\n" +
                "* Point two\n" +
                "## Step 1\n" +
                "## Step 2\n" +
                "## Step 3\n" +
                "* Point three\n" +
                "** Sub point 1\n" +
                "** Sub point 2\n\n\n" +
                "Well, that went well. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!" +
                "<a title=\"optional title\" href=\"http://www.textism.com\">This is a link</a>\n\n" +
                /*"table{border:1px solid black}.\n" +
                "|_. this|_. is|_. a|_. header|\n" +
                "<{background:gray}. |\\2. this is|{background:red;width:200px}. a|^<>{height:200px}. row|\n" +
                "|this|<>{padding:10px}. is|^. another|(bob#bob). row|\n\n" +*/
                "An image:\n\n" +
                "!/common/textist.gif(optional alt text)!\n\n" +
                "# Librarians rule\n" +
                "# Yes they do\n" +
                "# But you knew that\n\n" +
                "Some more text of dubious character. Here is a noisome string of CAPITAL letters. Here is something we want to <em>emphasize</em>.\n" +
                "That was a <b lang=\"en-US\">linebreak</b>. And something to indicate <strong>strength</strong>. Of course I could use <em>my own HTML tags</em> if I <strong>felt</strong> like it.\n\n" +
                "<h3>Coding</h3>\n" +
                "This <code>is some code, \"isn't it\"</code>. Watch those quote marks! Now for some preformatted text:\n\n" +
                "<pre>\n" +
                "<code>\n" +
	            "$text = str_replace(\"<p>%::%</p>\",\"\",$text);\n" +
	            "$text = str_replace(\"%::%</p>\",\"\",$text);\n" +
	            "$text = str_replace(\"%::%\",\"\",$text);\n" +
                "</code>\n" +
                "</pre>\n\n" +
                "This isn't code.\n\n" +
                "So you see, my friends:\n\n" +
                "* The time is now\n" +
                "* The time is not later\n" +
                "* The time is not yesterday\n" +
                "* We must act\n", 
                authoringEngine.Author(WikiMarkup, AuthoringScope.All));
        }
    }
}
