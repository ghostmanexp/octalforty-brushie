using System.Text.RegularExpressions;

using octalforty.Brushie.Diff;

namespace octalforty.Brushie.Test
{
    /// <summary>
    /// Allows <see cref="DiffEngine{T}"/> to work on a word-by-word basis.
    /// </summary>
    public class WordDataProvider : IDataProvider<string>
    {
        #region Private Constants
        private static readonly Regex wordRegex = new Regex(@"\b\w+?\b", RegexOptions.Multiline);
        #endregion

        #region Private Member Variables
        private MatchCollection matches;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the collection of <see cref="Match"/> objects
        /// for the current provider.
        /// </summary>
        public MatchCollection Matches
        {
            get { return matches; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="WordDataProvider"/> with
        /// a given source string.
        /// </summary>
        /// <param name="source"></param>
        public WordDataProvider(string source)
        {
            matches = wordRegex.Matches(source);
        }

        #region IDataProvider<string> Members
        /// <summary>
        /// Gets an value with the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
        {
            get { return matches[index].Value; }
        }

        /// <summary>
        /// Gets the number of items this <see cref="IDataProvider{T}"/> can supply.
        /// </summary>
        public int Count
        {
            get { return matches.Count; }
        }
        #endregion
    }
}
