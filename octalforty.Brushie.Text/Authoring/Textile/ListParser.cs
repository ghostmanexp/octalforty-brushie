using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing lists.
    /// </summary>
    public sealed class ListParser : BlockElementParserBase
    {
        #region Private Constants
        private static readonly Regex ListRegex =
            new Regex(
                @"(?<Expression>^((?<Qualifier>[*#]+)(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?\s(?<Title>.*)\r\n)+)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ListParser"/> class.
        /// </summary>
        public ListParser()
        {
        }

        #region ElementParserBase Members
        /// <summary>
        /// Returns a reference to the <see cref="System.Text.RegularExpressions.Regex"/>
        /// used in <see cref="ElementParserBase.Parse"/>.
        /// </summary>
        protected override Regex Regex
        {
            get { return ListRegex; }
        }

        /// <summary>
        /// Template method which is invoked from <see cref="ElementParserBase.Parse"/> when
        /// a match is encountered.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="match"></param>
        protected override void ProcessMatch(IAuthoringEngine authoringEngine, 
            DomElement parentElement, Match match)
        {
            Internal.List list = new Internal.List(match);
            BlockElementAttributes attributes = CreateBlockElementAttributes(match);

            parentElement.AppendChild(CreateList(authoringEngine, parentElement, list, attributes));
        }
        #endregion

        /// <summary>
        /// Creates a list.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="list"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static DomElement CreateList(IAuthoringEngine authoringEngine, DomElement parentElement, 
            Internal.List list, BlockElementAttributes attributes)
        {
            int index = 0;
            return CreateListRecursive(authoringEngine, parentElement, list, ref index, list.Items[index].Qualifier, attributes);
        }

        /// <summary>
        /// Recursively creates a list.
        /// </summary>
        /// <param name="authoringEngine"></param>
        /// <param name="parentElement"></param>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="previousQualifier"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static DomElement CreateListRecursive(IAuthoringEngine authoringEngine, 
            DomElement parentElement, Internal.List list, ref int index, string previousQualifier, 
            BlockElementAttributes attributes)
        {
            //
            // Creating DOM object
            List domList = previousQualifier[previousQualifier.Length - 1] == '#' ?
                (List)new OrderedList(parentElement, attributes) :
                new UnorderedList(parentElement, attributes);

            //
            // Iterating through items.
            for(; index < list.Items.GetLength(0); ++index)
            {
                //
                // Creating DOM list item and adding it to the 
                // list created above.
                ListItem listItem = new ListItem(domList, BlockElementAttributes.Empty);
                authoringEngine.ParseInlineElements(listItem, list.Items[index].Title);
                /*listItem.AppendChild(new TextBlock(listItem, InlineElementAttributes.Empty, 
                    list.Items[index].Title, TextBlockModifier.Unknown));*/

                domList.AppendChild(listItem);

                if(index < list.Items.GetLength(0) - 1)
                {
                    string nextQualifier = list.Items[index + 1].Qualifier;

                    if(nextQualifier.Length != previousQualifier.Length)
                    {
                        if(nextQualifier.Length > previousQualifier.Length)
                        {
                            ++index;
                            listItem.AppendChild(CreateListRecursive(authoringEngine, 
                                listItem, list, ref index, nextQualifier, BlockElementAttributes.Empty));
                        } // if
                        else
                            break;
                    } // if
                } // if
            } // for

            return domList;
        }
    }
}
