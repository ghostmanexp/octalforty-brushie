using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Textile;
using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    [TestFixture()]
    public class ComplexAuthoring
    {
        [Test()]
        public void Author()
        {
            #region Wiki Markup
            string WikiMarkup = "h2(head#h2){color:green}[en-US]<>(). This is a title\r\n\r\n" +
                                      "h3. This is a subhead\r\n\r\n" +
                                      "p{color:red;font-family:Tahoma;}. This is some text of *dubious _character_. Isn't* the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.\r\n\r\n" +
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
                                      "* The time is now\r\n" +
                                      "* The time is not later\r\n" +
                                      "* The time is not yesterday\r\n" +
                                      "* We must act\r\n\r\n" +
                                      "fn1. Footnote.\r\n\r\n".Replace("\r\n\r\n", "\t").Replace("\r\n", "").Replace("\t", "\r\n\r\n");
            #endregion
            
            TextileParser.AddElementParser(new ParagraphParser());
            TextileParser.AddElementParser(new HeadingParser());
            TextileParser.AddElementParser(new BlockquoteParser());
            TextileParser.AddElementParser(new ListParser());
            TextileParser.AddElementParser(new ImageParser());
            TextileParser.AddElementParser(new TextParser());

            Document document = TextileParser.Parse(AuthoringScope.All, WikiMarkup.ToString());
            
            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();
            document.Accept(htmlAuthoringDomElementVisitor);

            Assert.AreEqual("<h2 class=\"head\" id=\"h2\" lang=\"en-US\" style=\"color:green;text-align: justify;padding-left: 1em;padding-right: 1em;\">This is a title</h2><h3>This is a subhead</h3><p style=\"color:red;font-family:Tahoma;\"><span>This is some text of </span><strong>dubious _character_. Isn't</strong><span> the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.</span></p><blockquote lang=\"fr\"><span>This is a block quote. I'll admit it's not the most exciting block quote ever devised.</span></blockquote><p><span>Simple list:</span></p><ol style=\"color:blue;\"><li><span>one</span></li><li><span>two</span></li><li><span>three</span></li></ol><span>\r\n" +
                "</span><p><span>Multi-level list:</span></p><ol><li><span>one</span><ol><li><span>aye</span></li><li><span>bee</span></li><li><span>see</span></li></ol></li><li><span>two</span><ol><li><span>x</span></li><li><span>y</span></li></ol></li><li><span>three</span></li></ol><span>\r\n" +
                "</span><p><span>Mixed list:</span></p><ul><li><span>Point one</span></li><li><span>Point two</span><ol><li><span>Step 1</span></li><li><span>Step 2</span></li><li><span>Step 3</span></li></ol></li><li><span>Point three</span><ul><li><span>Sub point 1</span></li><li><span>Sub point 2</span></li></ul></li></ul><span>\r\n\r\n" +
                "</span><p><span>Well, that went well[1]. How about we insert an <a href=\"/\" title=\"watch out\">old-fashioned hypertext link</a>? Will the quote marks in the tags get messed up? No!\"This is a link (optional title)\":http://www.textism.com</span></p><p><span>An image:</span></p><p><img src=\"http://www.google.com/intl/en_ALL/images/logo.gif\" alt=\"Google\" /></p><ol><li><span>Librarians rule</span></li><li><span>Yes they do</span></li><li><span>But you knew that</span></li></ol><span>\r\n" +
                "</span><p><span>Some more text of dubious character. Here is a noisome string of CAPITAL letters. Here is something we want to </span><em>emphasize</em><span>.\r\n" +
                "p. That was a </span><strong>*[en-US]linebreak</strong><strong>. And something to indicate </strong><span>strength*. Of course I could use <em>my own HTML tags</em> if I <strong>felt</strong> like it.</span></p><h3>Coding</h3><p><span>This <code>is some code, \"isn't it\"</code>. Watch those quote marks! Now for some preformatted text:</span></p><p><span><pre class=\"test\">\r\n" +
                "$text = str_replace(\"<p></span><span>::</span><span></p>\",\"\",$text);\r\n" +
                "$text = str_replace(\"</span><span>::</span><span></p>\",\"\",$text);\r\n" +
                "$text = str_replace(\"</span><span>::</span><span>\",\"\",$text);\r\n" +
                "</pre></span></p><p><span>This isn't code.</span></p><p><span>So you see, my friends:</span></p><ul><li><span>The time is now</span></li><li><span>The time is not later</span></li><li><span>The time is not yesterday</span></li><li><span>We must act</span></li></ul><span>\r\n" +
                "fn1. Footnote.\r\n\r\n</span>",
                htmlAuthoringDomElementVisitor.Html.ToString());
        }
    }
}
