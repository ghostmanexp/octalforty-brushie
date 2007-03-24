using System;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Represents an enclosure, which encloses the text with simple HTML tags.
    /// </summary>
    public class HtmlTagEnclosure : Enclosure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HtmlTagEnclosure"/> class with a given
        /// tag (e.g., <c>b</c>, <c>del</c>).
        /// </summary>
        /// <param name="tag"></param>
        public HtmlTagEnclosure(String tag) :
            base(String.Format("<{0}>", tag), String.Format("</{0}>", tag))
        {
        }
    }
}
