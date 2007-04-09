﻿using System;

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
        [Test()]
        public void Author()
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
                                          /*"* Point one\r\n" +
                                          "* Point two\r\n" +
                                          "## Step 1\r\n" +
                                          "## Step 2\r\n" +
                                          "## Step 3\r\n" +
                                          "* Point three\r\n" +
                                          "** Sub point 1\r\n" +*/
                                          "** Sub point 2\r\n\r\n\r\n" +
                                          "Well, that went well. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!" +
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
                                          "<pre>\r\n" +
                                          "$text = str_replace(\"<p>%::%</p>\",\"\",$text);\r\n" +
                                          "$text = str_replace(\"%::%</p>\",\"\",$text);\r\n" +
                                          "$text = str_replace(\"%::%\",\"\",$text);\r\n" +
                                          "</pre>\r\n\r\n" +
                                          "This isn't code.\r\n\r\n" +
                                          "So you see, my friends:\r\n\r\n" +
                                          /*"* The time is now\r\n" +
                                          "* The time is not later\r\n" +
                                          "* The time is not yesterday\r\n" +*/
                                          "* We must act\r\n";
            #endregion

            TextileAuthoringEngine authoringEngine = 
                new TextileAuthoringEngine(new HtmlTextileAuthoringFormatter());

            string htmlMarkup = authoringEngine.Author(WikiMarkup, AuthoringScope.All);
            Assert.AreEqual("<h2 class=\"head\" id=\"h2\" lang=\"en-US\" style=\"color:green;text-align: justify;padding-left: 1em;padding-right: 1em;\">This is a title</h2>\r\n\r\n" +
                "<h3>This is a subhead</h3>\r\n\r\n" +
                "<p style=\"color:red;\">This is some text of dubious character. Isn't the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.</p>\r\n\r\n" +
                "<blockquote lang=\"fr\">This is a block quote. I'll admit it's not the most exciting block quote ever devised.</blockquote>\r\n\r\n" +
                "<p>Simple list:</p>\r\n\r\n" +
                "#{color:blue} one" +
                "# two" +
                "# three\r\n\r\n" +
                "<p>Multi-level list:</p>\r\n\r\n" +
                "# one" +
                "## aye" +    
                "## bee" +
                "## see" +
                "# two" +
                "## x" +
                "## y" +
                "# three\r\n\r\n" +
                "<p>Mixed list:</p>\r\n\r\n" +
                /*"* Point one" +
                "* Point two" +
                "## Step 1" +
                "## Step 2" +
                "## Step 3" +
                "* Point three" +
                "** Sub point 1" +*/
                "** Sub point 2\r\n\r\n" +
                "<p>Well, that went well. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!" +
                "<a title=\"optional title\" href=\"http://www.textism.com\">This is a link</a></p>\r\n\r\n" +
                /*"table{border:1px solid black}.\r\n" +
                "|_. this|_. is|_. a|_. header|\r\n" +
                "<{background:gray}. |\\2. this is|{background:red;width:200px}. a|^<>{height:200px}. row|\r\n" +
                "|this|<>{padding:10px}. is|^. another|(bob#bob). row|\r\n\r\n" +*/
                "<p>An image:</p>\r\n\r\n" +
                "<p><img alt=\"Google\" src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" /></p>\r\n\r\n" +
                "# Librarians rule" +
                "# Yes they do" +
                "# But you knew that\r\n\r\n" +
                "<p>Some more text of dubious character. Here is a noisome string of CAPITAL letters. Here is something we want to <em>emphasize</em>." +
                "That was a <b lang=\"en-US\">linebreak</b>. And something to indicate <strong>strength</strong>. Of course I could use <em>my own HTML tags</em> if I <strong>felt</strong> like it.</p>\r\n\r\n" +
                "<h3>Coding</h3>\r\n\r\n" +
                "<p>This <code>is some code, \"isn't it\"</code>. Watch those quote marks! Now for some preformatted text:</p>\r\n\r\n" +
                "<pre>\r\n" +
                "$text = str_replace(\"&lt;p&gt;%::%&lt;/p&gt;\",\"\",$text);\r\n" +
                "$text = str_replace(\"%::%&lt;/p&gt;\",\"\",$text);\r\n" +
	            "$text = str_replace(\"%::%\",\"\",$text);\r\n" +
                "</pre>\r\n\r\n" +
                "<p>This isn't code.</p>\r\n\r\n" +
                "<p>So you see, my friends:</p>\r\n\r\n" +
               /* "* The time is now\r\n" +
                "* The time is not later\r\n" +
                "* The time is not yesterday\r\n" +*/
                "* We must act", 
                htmlMarkup);
        }

        [Test()]
        public void Author2()
        {
            const String WikiMarkup =
                "The metrics for+{color:green;}clarity+-{color:red;}obfuscation-are+{color:green;}more\\-well+-{color:red;}more\\-or\\-well-understood. Do you have a game plan to become+{color:green;}peerlessly synergetic across all platforms?+-{color:red;}cross\\-media?-Think interactive.-{color:red;}True.-Really.+{color:green;}Astonishing.+";
            const String HtmlMarkup =
                "The metrics for<ins style=\"color:green;\">clarity</ins><del style=\"color:red;\">obfuscation</del>are<ins style=\"color:green;\">more-well</ins><del style=\"color:red;\">more-or-well</del>understood. Do you have a game plan to become<ins style=\"color:green;\">peerlessly synergetic across all platforms?</ins><del style=\"color:red;\">cross-media?</del>Think interactive.<del style=\"color:red;\">True.</del>Really.<ins style=\"color:green;\">Astonishing.</ins>";

            TextileAuthoringEngine authoringEngine =
                new TextileAuthoringEngine(new HtmlTextileAuthoringFormatter());

            string effectiveHtmlMarkup = authoringEngine.Author(WikiMarkup, AuthoringScope.All);
            Assert.AreEqual(HtmlMarkup, effectiveHtmlMarkup);
        }
    }
}