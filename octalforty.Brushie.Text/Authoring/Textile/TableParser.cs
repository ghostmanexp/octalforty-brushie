using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    public sealed class TableParser
    {
        #region Private Constants
        private static readonly Regex ExplicitTableRegex = new Regex(
            @"(?<Expression>^(table(\(((\#(?<ID>.+?))|((?<CssClass>.+?)\#(?<ID>.+?))|(?<CssClass>.+?))\))?(\{(?<Style>.+?)\})?(\[(?<Language>.+?)\])?.\r\n))",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion
    }
}
