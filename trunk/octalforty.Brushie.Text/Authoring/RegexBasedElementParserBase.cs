using System;
using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Provides a base class for element parsers which use regular expressions when parsing markup.
    /// </summary>
    public abstract class RegexBasedElementParserBase : ElementParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RegexBasedElementParserBase"/> class.
        /// </summary>
        protected RegexBasedElementParserBase()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/>.
        /// </summary>
        /// <param name="authoringEngine">
        /// The <see cref="IAuthoringEngine"/> which initiated the parsing process.
        /// </param>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(IAuthoringEngine authoringEngine, DomElement parentElement, string text)
        {
            Match match = Regex.Match(text);
            if(match.Success)
            {
                int startIndex = 0;
                while(match.Success)
                {
                    //
                    // Parsing prefix...
                    if(startIndex < match.Index)
                    {
                        ParseWithNextElementParser(authoringEngine, parentElement,
                            text.Substring(startIndex, match.Index - startIndex));
                    } // if

                    ProcessMatch(authoringEngine, parentElement, match);

                    startIndex = match.Index + match.Length;
                    match = Regex.Match(text, startIndex);
                } // while

                ParseWithNextElementParser(authoringEngine, parentElement, text.Substring(startIndex));
            } // if
            else
            {
                //
                // Proceed.
                ParseWithNextElementParser(authoringEngine, parentElement, text);
            } // else
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="Parse"/>.
        /// </summary>
        protected virtual Regex Regex
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Template method which is invoked from <see cref="Parse"/> when
        /// a match is encountered.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="match"></param>
        protected virtual void ProcessMatch(IAuthoringEngine authoringEngine,
            DomElement parentElement, Match match)
        {
        }
        #endregion
    }
}
