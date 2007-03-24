using System;
using System.Text;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Textile authoring formatter, which produces HTML output.
    /// </summary>
    public class HtmlTextileAuthoringFormatter : ITextileAuthoringFormatter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ITextileAuthoringFormatter"/> class.
        /// </summary>
        public HtmlTextileAuthoringFormatter()
        {
        }

        #region ITextileAuthoringFormatter Members
        /// <summary>
        /// Formats a heading of a given level with provided attributes.
        /// </summary>
        /// <param name="level">Heading level.</param>
        /// <param name="text">Heading text.</param>
        /// <param name="attributes">Attributes of the heading block element.</param>
        /// <returns></returns>
        public string FormatHeading(Int32 level, String text, BlockElementAttributes attributes)
        {
            String tag = String.Format("h{0}", level);
            return String.Format("{0}{1}</{0}>", GetStartTag(tag, attributes), text);
        }
        #endregion

        /// <summary>
        /// Creates a start tag of a form "&lt;<paramref name="tag"/> id="<see cref="BlockElementAttributes.ID"/>"
        /// class="<see cref="BlockElementAttributes.CssClass"/>" style="<see cref="BlockElementAttributes.Style"/>"
        /// lang="<see cref="BlockElementAttributes.Language"/>">".
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        protected static String GetStartTag(String tag, BlockElementAttributes attributes)
        {
            StringBuilder tagBuilder = new StringBuilder();
            tagBuilder.AppendFormat("<{0}", tag);

            if(!String.IsNullOrEmpty(attributes.CssClass))
                tagBuilder.AppendFormat(" class=\"{0}\"", attributes.CssClass);

            if(!String.IsNullOrEmpty(attributes.Language))
                tagBuilder.AppendFormat(" lang=\"{0}\"", attributes.Language);

            //
            // Now we need to construct a new "style" attribute since values
            // in Alignment, LeftIndent and RightIndent may add something new.
            // On the other hand, they may not be specified at all, so
            // this should be taken into consideration too.
            if(attributes.Alignment != BlockElementAlignment.Unknown ||
                attributes.LeftIndent != 0 || attributes.RightIndent != 0 ||
                !String.IsNullOrEmpty(attributes.Style))
            {
                String style = String.Empty;
                if(!String.IsNullOrEmpty(attributes.Style))
                {
                    style += attributes.Style;
                    if(!style.EndsWith(";"))
                        style += ";";
                } // if

                if(attributes.Alignment != BlockElementAlignment.Unknown)
                {
                    style += String.Format("text-align: {0};",
                        attributes.Alignment.ToString().ToLower());
                } // if

                if(attributes.LeftIndent != 0)
                {
                    style += String.Format("padding-left: {0};",
                        attributes.LeftIndent);
                } // if

                if(attributes.RightIndent != 0)
                {
                    style += String.Format("padding-right: {0};",
                        attributes.RightIndent);
                } // if
                
                tagBuilder.AppendFormat(" style=\"{0}\"", style);
            } // if

            return tagBuilder.ToString();
        }
    }
}
