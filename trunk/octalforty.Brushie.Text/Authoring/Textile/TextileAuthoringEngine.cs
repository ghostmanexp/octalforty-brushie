using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile
{
    /// <summary>
    /// Represents an authoring engine capable of transforming Textile-formatted text
    /// into arbitrary markup with the help of <see cref="ITextileAuthoringFormatter"/>.
    /// </summary>
    public sealed class TextileAuthoringEngine
    {
        #region Private Constants
        /// <summary>
        ///  Regular expression built for C# on: Сб, мар 24, 2007, 07:22:12 
        ///  Using Expresso Version: 2.1.2150, http://www.ultrapico.com
        ///  
        ///  A description of the regular expression:
        ///  
        ///  [Heading]: A named capture group. [^h(?<Level>[1-6])(\((?<CssClass>.+?)\))?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>(\(*\)*))?\.\s(?<Text>.*)\r\n\r\n]
        ///      ^h(?<Level>[1-6])(\((?<CssClass>.+?)\))?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>(\(*\)*))?\.\s(?<Text>.*)\r\n\r\n
        ///          Beginning of line or string
        ///          h
        ///          [Level]: A named capture group. [[1-6]]
        ///              Any character in this class: [1-6]
        ///          [1]: A numbered capture group. [\((?<CssClass>.+?)\)], zero or one repetitions
        ///              \((?<CssClass>.+?)\)
        ///                  (
        ///                  [CssClass]: A named capture group. [.+?]
        ///                      Any character, one or more repetitions, as few as possible
        ///                  )
        ///          [Alignment]: A named capture group. [(=)|(\<\>)|(\<)|(\>)], zero or one repetitions
        ///              Select from 4 alternatives
        ///                  [2]: A numbered capture group. [=]
        ///                      =
        ///                  [3]: A numbered capture group. [\<\>]
        ///                      \<\>
        ///                          <
        ///                          >
        ///                  [4]: A numbered capture group. [\<]
        ///                      <
        ///                  [5]: A numbered capture group. [\>]
        ///                      >
        ///          [Indentation]: A named capture group. [(\(*\)*)], zero or one repetitions
        ///              [6]: A numbered capture group. [\(*\)*]
        ///                  \(*\)*
        ///                      (, any number of repetitions
        ///                      ), any number of repetitions
        ///          .
        ///          Whitespace
        ///          [Text]: A named capture group. [.*]
        ///              Any character, any number of repetitions
        ///          Carriage return
        ///          New line
        ///          Carriage return
        ///          New line
        ///  
        ///  
        /// </summary>
        public static readonly Regex HeadingRegex = new Regex(
            @"(?<Heading>^h(?<Level>[1-6])(\((?<CssClass>.+?)\))?(?<Alignment>(=)|(\<\>)|(\<)|(\>))?(?<Indentation>(\(*\)*))?\.\s(?<Text>.*)\r\n\r\n)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | 
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        #endregion

    }
}
