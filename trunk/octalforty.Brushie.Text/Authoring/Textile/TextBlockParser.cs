using octalforty.Brushie.Text.Authoring.Textile;
using octalforty.Brushie.Text.Authoring.Textile.Dom;

namespace octalforty.Brushie.Text.Authoring.Textile
{
	/// <summary>
	/// Provides functionality for parsing text blocks.
	/// </summary>
	public class TextBlockParser : InlineElementParserBase
	{
		/// <summary>
		/// Initializes a new instance of <see cref="TextBlockParser"/> class.
		/// </summary>
		public TextBlockParser()
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
			parentElement.AppendChild(new TextBlock(parentElement, InlineElementAttributes.Empty, 
				text, TextBlockModifier.Unknown));
		}
		#endregion
	}
}
