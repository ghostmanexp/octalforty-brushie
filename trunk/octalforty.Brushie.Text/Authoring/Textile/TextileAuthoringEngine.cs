using System;

using octalforty.Brushie.Text.Authoring.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Authoring engine which parses Textile markup.
    /// </summary>
    /// <remarks>
    /// Textile Quick Reference is available at http://hobix.com/textile/quick.html. More extensive one
    /// can be found at http://www.textism.com/tools/textile/.<para />
    /// <b>Block modifier syntax:</b><para />
    /// <list type="bullet">
    ///     <item>
    ///         Heading: <c>h(1-6).</c><br />
    ///         Paragraphs beginning with <c>'hn. '</c> (where n is 1-6) are parsed into <see cref="Heading"/> objects.<br />
    ///         Example: <code>h1. Header...</code>
    ///     </item>
    ///     <item>
    ///         Paragraph: <c>p.</c> (also applied by default)<br />
    ///         Paragraphs are parsed into <see cref="Paragraph"/> objects.<br />
    ///         Example: <code>p. Text</code>
    ///     </item>
    ///     <item>
    ///         Blockquote: <c>bq.</c><br />
    ///         Blockquotes are parsed into <see cref="Blockquote"/> objects.<br />
    ///         Example: <code>Example: bq. Block quotation...</code>
    ///     </item>
    ///     <item>
    ///         Blockquote with citation: <c>bq.:http://citation.url</c><br />
    ///         Blockquotes with citations are parsed into <see cref="Blockquote"/> objects.<br />
    ///         Example: <code>bq.:http://textism.com/ Text...</code>
    ///     </item>
    ///     <item>
    ///         Footnote: <c>fn(1-100).</c><br />
    ///         Footnotes are parsed into <see cref="Footnote"/> objects.<br />
    ///         Example: <code>fn1. Footnote...</code>
    ///     </item>
    ///     <item>
    ///         Numeric list: <c>#</c>, <c>##</c><br />
    ///         Consecutive paragraphs beginning with <c>#</c> are parsed into <see cref="OrderedList"/>
    ///         and <see cref="ListItem"/> objects.<br />
    ///         Example:
    ///         <code>
    ///             # This is
    ///             # An ordered
    ///             ## List
    ///         </code>
    ///     </item>
    ///     <item>
    ///         Bulleted list: <c>*</c>, <c>**</c><br />
    ///         Consecutive paragraphs beginning with <c>*</c> are parsed into <see cref="UnorderedList"/>
    ///         and <see cref="ListItem"/> objects.<br />
    ///         Example:
    ///         <code>
    ///             * This is
    ///             * An unordered
    ///             ** List
    ///         </code>
    ///     </item>
    /// </list>
    /// <b>Phrase modifier syntax:</b><br />
    /// <list type="table">
    ///     <listheader>
    ///         <term>Text block of this pattern...</term>
    ///         <description>
    ///             ...are parsed into <see cref="TextBlock"/> objects with this <see cref="TextBlockFormatting"/>
    ///         </description>
    ///     </listheader>
    ///     <item>
    ///         <term><c>_emphasis_</c></term>
    ///         <description><see cref="TextBlockFormatting.Emphasis"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>__italic__</c></term>
    ///         <description><see cref="TextBlockFormatting.Italics"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>*strong*</c></term>
    ///         <description><see cref="TextBlockFormatting.StrongEmphasis"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>**bold**</c></term>
    ///         <description><see cref="TextBlockFormatting.Bold"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>??citation??</c></term>
    ///         <description><see cref="TextBlockFormatting.Citation"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>-deleted text-</c></term>
    ///         <description><see cref="TextBlockFormatting.Deleted"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>+inserted text+</c></term>
    ///         <description><see cref="TextBlockFormatting.Inserted"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>^superscript^</c></term>
    ///         <description><see cref="TextBlockFormatting.Superscript"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>~subscript~</c></term>
    ///         <description><see cref="TextBlockFormatting.Subscript"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>@code@</c></term>
    ///         <description><see cref="TextBlockFormatting.Code"/></description>
    ///     </item>
    ///     <item>
    ///         <term><c>%(bob)span%</c></term>
    ///         <description>
    ///             All styles are applied, <see cref="TextBlock.Formatting"/> equals 
    ///             <see cref="TextBlockFormatting.Span"/>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>==notextile==</c></term>
    ///         <description>
    ///             The text is parsed into <see cref="TextBlock"/> and left without further parsing 
    ///             with <see cref="TextBlock.Formatting"/> set to <see cref="TextBlockFormatting.Unknown"/>
    ///         </description>
    ///     </item>
    /// </list>
    /// <b>Hyperlinks</b><br />
    /// Hyperlink: <c>"Hyperlink text(Optional title)":Url</c><br />
    /// Hyperlinks are parsed into <see cref="Hyperlink"/> objects.<br />
    /// <b>Images</b><br />
    /// Image: <c>!ImageUrl(Optional alternate text)!</c><br />
    /// Images are parsed into <see cref="Image"/> objects.<br />
    /// <b>Acronyms</b><br />
    /// Acronym: <c>ABC(Always Be Closing)</c><br />
    /// Acronyms are parsed into <see cref="Acronym"/> objects.<br />
    /// <b>Tables</b><br />
    /// Simple tables:
    /// <code>
    ///     |a|simple|table|row|
    ///     |And|Another|table|row|
    /// </code>
    /// <code>
    ///     |_. A|_. table|_. header|_.row|
    ///     |A|simple|table|row|
    /// </code>
    /// Tables with attributes:
    /// <code>
    ///     table{border:1px solid black}.
    ///     {background:#ddd;color:red}. |{}| | | |
    /// </code>
    /// <b>Applying Attributes</b><br />
    /// Most anywhere Textile code is used, attributes such as arbitrary css style, CSS classes, and ids can be applied. 
    /// The syntax is fairly consistent.<para />
    /// The following characters quickly alter the alignment of block elements:
    /// <list type="table">
    ///     <item>
    ///         <term><c>&lt;</c></term>
    ///         <description>
    ///             <see cref="BlockElementAlignment.Left"/>.<br />
    ///             Example: <c>p&lt;. Text</c> - left-aligned paragraph.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>&gt;</c></term>
    ///         <description>
    ///             <see cref="BlockElementAlignment.Right"/>.<br />
    ///             Example: <c>h3&gt;. Heading</c> - right-aligned heading 3.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>=</c></term>
    ///         <description>
    ///             <see cref="BlockElementAlignment.Center"/>.<br />
    ///             Example: <c>p=. Text</c> - centered-aligned paragraph.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>&lt;&gt;</c></term>
    ///         <description>
    ///             <see cref="BlockElementAlignment.Justify"/>.<br />
    ///             Example: <c>p&lt;&gt;. Text</c> - justified paragraph.
    ///         </description>
    ///     </item>
    /// </list>
    /// These will change vertical alignment in table cells:
    /// <list type="table">
    ///     <item>
    ///         <term><c>^</c></term>
    ///         <description>
    ///             <see cref="TableCellAlignment.Top"/>.<br />
    ///             Example: <c>|^. top-aligned table cell|</c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>-</c></term>
    ///         <description>
    ///             <see cref="TableCellAlignment.Middle"/>.<br />
    ///             Example: <c>|-. middle-aligned table cell|</c>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>~</c></term>
    ///         <description>
    ///             <see cref="TableCellAlignment.Bottom"/>.<br />
    ///             Example: <c>|~. bottom-aligned table cell|</c>
    ///         </description>
    ///     </item>
    /// </list>
    /// Plain <c>(parentheses)</c> inserted between block syntax and the closing dot-space 
    /// indicate CSS classes and ids, which are parsed into <see cref="InlineElementAttributes.CssClass"/>
    /// and <see cref="InlineElementAttributes.ID"/>, respecively.
    /// <list type="table">
    ///     <item>
    ///         <term><c>p(hector). paragraph</c></term>
    ///         <description>
    ///             <see cref="Paragraph"/>'s <see cref="InlineElementAttributes.CssClass"/> equals <c>hector</c>.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><c>p(#fluid). paragraph</c></term>
    ///         <description>
    ///             <see cref="Paragraph"/>'s <see cref="InlineElementAttributes.ID"/> equals <c>fluid</c>.
    ///         </description>
    ///     </item>
    ///      <item>
    ///         <term><c>p(hector#fluid). paragraph</c></term>
    ///         <description>
    ///             CSS classes and ids can be combined. In this case, <see cref="Paragraph"/>'s 
    ///             <see cref="InlineElementAttributes.ID"/> equals <c>fluid</c> and
    ///             <see cref="InlineElementAttributes.CssClass"/> equals <c>hector</c>.
    ///         </description>
    ///     </item>
    /// </list>
    /// Curly <c>{brackets}</c> insert arbitrary CSS styles, which are parsed into 
    /// <see cref="InlineElementAttributes.Style"/>.<br />
    /// Examples:
    /// <code>
    ///     p{line-height:18px}. paragraph
    ///     h3{color:red}. header 3
    /// </code>
    /// Square <c>[brackets]</c> insert language attributes, which are parsed into 
    /// <see cref="InlineElementAttributes.Language"/>.<br />
    /// Examples:
    /// <code>
    ///     p[no]. paragraph
    ///     %[fr]phrase%
    ///</code>
    /// Usually Textile block element syntax requires a dot and space before the block
    /// begins, but since lists don't, they can be styled just using braces:
    /// <code>
    ///     #{color:blue} one
    ///     # big
    ///     # list
    /// </code>
    /// Using the <c>%</c> tag to style a phrase:
    /// <code>
    ///     It goes like this, %{color:red}the fourth the fifth%.
    /// </code>              
    /// </remarks>    
    public class TextileAuthoringEngine : AuthoringEngineBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TextileAuthoringEngine"/>.
        /// </summary>
        public TextileAuthoringEngine()
        {
            //
            // Adding basic Textile parsers.
            AddBlockElementParser(new NoTextileParser());
            AddBlockElementParser(new ParagraphParser());
            AddBlockElementParser(new BlockquoteParser());
            AddBlockElementParser(new HeadingParser());
            AddBlockElementParser(new ImageParser());
            AddBlockElementParser(new ListParser());
            AddBlockElementParser(new FootnoteParser());

            AddBlockElementParser(new BlockElementFallbackParser());

            AddInlineElementParser(new AcronymParser());
            AddInlineElementParser(new HyperlinkParser());
            AddInlineElementParser(new FootnoteReferenceParser());
            AddInlineElementParser(new FormattedTextBlockParser());
            AddInlineElementParser(new UnformattedTextBlockParser());
        }

        /// <summary>
        /// Performs parsing of <paramref name="text"/> and converts it to HTML
        /// using <see cref="HtmlAuthoringDomElementVisitor"/>.
        /// </summary>
        /// <param name="text"></param>
        public String Author(String text)
        {
            DomDocument document = Parse(text);

            HtmlAuthoringDomElementVisitor htmlAuthoringDomElementVisitor = new HtmlAuthoringDomElementVisitor();
            document.Accept(htmlAuthoringDomElementVisitor);

            return htmlAuthoringDomElementVisitor.Html;
        }
    }
}
