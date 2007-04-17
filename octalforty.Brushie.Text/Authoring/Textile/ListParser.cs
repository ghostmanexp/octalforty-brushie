using System;
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
                parentElement.AppendChild(CreateList(parentElement, list));

                //
                // ...and finally parse suffux
                ParseSuffix(parentElement, authoringScope, text, listMatch);
                return;
            } // if

            ParseWithNextElementParser(parentElement, authoringScope, text);
        }
        #endregion

        private DomElement CreateList(DomElement parentElement, Internal.List list)
        {
            int index = 0;
            string previousQualifier = list.Items[index].Qualifier;
            return CreateListRecursive(parentElement, list, ref index, ref previousQualifier);
        }

        private DomElement CreateListRecursive(DomElement parentElement, Internal.List list, ref int index, ref string previousQualifier)
        {
            List domList = previousQualifier[previousQualifier.Length - 1] == '#' ?
                   (List)new OrderedList(parentElement, BlockElementAttributes.Empty) :
                   new UnorderedList(parentElement, BlockElementAttributes.Empty);

            for(; index < list.Items.GetLength(0); ++index)
            {
                ListItem listItem = new ListItem(domList, BlockElementAttributes.Empty);
                listItem.AppendChild(new TextBlock(listItem, list.Items[index].Title));

                domList.AppendChild(listItem);

                if(index < list.Items.GetLength(0) - 1)
                {
                    string nextQualifier = list.Items[index + 1].Qualifier;

                    if(nextQualifier.Length != previousQualifier.Length)
                    {
                        if(nextQualifier.Length > previousQualifier.Length)
                        {
                            ++index;
                            listItem.AppendChild(CreateListRecursive(listItem, list, ref index, ref nextQualifier));
                        } // if
                        else
                        {
                            break;
                        } // else
                    } // if
                } // if

                //previousQualifier = currentQualifier;
            } // for

            return domList;
        }
    }
}
