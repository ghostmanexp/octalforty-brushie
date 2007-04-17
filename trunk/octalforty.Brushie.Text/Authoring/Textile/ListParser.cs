using System.Text.RegularExpressions;

using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Provides functionality for parsing lists.
    /// </summary>
    public sealed class ListParser : ElementParserBase
    {
        #region Private Constants
        private static readonly Regex ListRegex = new Regex(
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
        /// Parses <paramref name="text"/> which is the child of <paramref name="parentElement"/> in
        /// accordance with <paramref name="authoringScope"/>.
        /// </summary>
        /// <param name="parentElement">Parent DOM element.</param>
        /// <param name="authoringScope">Authoring scope.</param>
        /// <param name="text">The text to be parsed.</param>
        public override void Parse(DomElement parentElement, AuthoringScope authoringScope, string text)
        {
            Match listMatch = ListRegex.Match(text);
            if(listMatch.Success)
            {
                //
                // Parsing prefix...
                ParsePrefix(parentElement, authoringScope, text, listMatch);

                //
                // ...list itself...
                Internal.List list = new Internal.List(listMatch);
                BlockElementAttributes attributes = CreateBlockElementAttributes(listMatch);

                parentElement.AppendChild(CreateList(parentElement, list, attributes));

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, listMatch);
                return;
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion

        /// <summary>
        /// Creates a list.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="list"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static DomElement CreateList(DomElement parentElement, Internal.List list, BlockElementAttributes attributes)
        {
            int index = 0;
            return CreateListRecursive(parentElement, list, ref index, list.Items[index].Qualifier, attributes);
        }

        /// <summary>
        /// Recursively creates a list.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="previousQualifier"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static DomElement CreateListRecursive(DomElement parentElement, Internal.List list, 
            ref int index, string previousQualifier, BlockElementAttributes attributes)
        {
            //
            // Creating DOM object
            List domList = 
                previousQualifier[previousQualifier.Length - 1] == '#' ?
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
                listItem.AppendChild(new TextBlock(listItem, InlineElementAttributes.Empty, 
                    list.Items[index].Title, TextBlockModifier.Unknown));

                domList.AppendChild(listItem);

                if(index < list.Items.GetLength(0) - 1)
                {
                    string nextQualifier = list.Items[index + 1].Qualifier;

                    if(nextQualifier.Length != previousQualifier.Length)
                    {
                        if(nextQualifier.Length > previousQualifier.Length)
                        {
                            ++index;
                            listItem.AppendChild(CreateListRecursive(listItem, list, ref index, 
                                nextQualifier, BlockElementAttributes.Empty));
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
