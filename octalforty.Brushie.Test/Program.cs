using System;
using System.Diagnostics;
using System.Text;

using octalforty.Brushie.Diff;
using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.Test
{
    class Program
    {
        #region Wiki Markup
        const String WikiMarkup = "h2(head#h2){color:green}[en-US]<>(). This is a title\r\n\r\n" +
                                      "h3. This is a subhead\r\n\r\n" +
                                      "p{color:red;font-family:Tahoma;}. This is some text of *dubious _character_. Isn't* the use of \"quotes\" just lazy writing -- and theft of 'intellectual property' besides? I think the time has come to see a block quote.\r\n\r\n" +
                                      "bq[fr]. This is a block quote. I'll admit it's not the most exciting block quote ever devised.\r\n\r\n" +
                                      "Simple list:\r\n\r\n" +
                                      "#{color:blue} one\r\n" +
                                      "# two (and \"This is a link (optional title)\":http://www.textism.com )\r\n" +
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
                                      "fn1. Footnote.\r\n\r\n";
        #endregion

        static void Main()
        {
            /*string w = "ѕосле того, как вам был открыт *доступ*, войти в _систему_ можно войти следующим образом. ¬ верхней части \"главной страницы\":http://www.bronni.ru в области под названием \"¬ход дл€ клиентов\" наход€тс€ два текстовых пол€, куда необходимо ввести логин и пароль. ѕосле этого нажмите на ссылку \"¬ойти\".\r\n\r\n" +
                "≈сли все прошло удачно, то %{background:#FFEA77}название ??области?? *должно ^помен€тьс€^ на* \"¬ход выполнен\"%. ≈сли такого не происходит, то обратите внимание на область, расположенную немного левее - там вывод€тс€ системные сообщени€. ≈сли вам сообщаетс€ о том, что вы ввели неверный логин или пароль, то вы можете воспользватьс€ формой \"напоминани€ парол€(Ќапоминани€ парол€)\":http://www.bronni.ru/default.aspx?page=passwordreminderpage.ascx. ≈сли же вам сообщаетс€, что \"ѕользователь с таким именем уже работает с базой данных\", то поставьте галочку на пункте \"¬ойти принудительно\", еще раз введите пароль и нажмите \"¬ойти\".\r\n\r\n" +
                "Ќу и если ничего не помогает, то \"св€житесь с нами\":http://www.bronni.ru/default.aspx?page=contactspage.ascx\r\n\r\n";

            TextileAuthoringEngine textileAuthoringEngine = new TextileAuthoringEngine();
            string h = textileAuthoringEngine.Author(w);*/

            StringBuilder wiki = new StringBuilder();
            for(int i = 0; i < 1; ++i)
                wiki.Append(WikiMarkup);

            IAuthoringEngine authoringEngine = new TextileAuthoringEngine();

            Stopwatch stopwatch = Stopwatch.StartNew();
            DomDocument document = authoringEngine.Parse(wiki.ToString());
            stopwatch.Stop();
            Console.WriteLine("{0} bytes in {1} sec.", wiki.ToString().Length, 
                stopwatch.ElapsedMilliseconds / 1000.0);

            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();
            document.Accept(htmlAuthoringDomElementVisitor);
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
