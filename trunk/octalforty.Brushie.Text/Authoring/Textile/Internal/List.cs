#if FW1
using System.Collections;
#else
using System.Collections.Generic;
#endif

using System.Text.RegularExpressions;

namespace octalforty.Brushie.Text.Authoring.Textile.Internal
{
    /// <summary>
    /// Represents a list.
    /// </summary>
    internal sealed class List : Element
    {
        #region Private Member Variables
        private ListItem[] items;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets an array of <see cref="ListItem"/> objects which represent items of this <see cref="List"/>.
        /// </summary>
        public ListItem[] Items
        {
            get { return items; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="List"/> class.
        /// </summary>
        public List()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="List"/> class.
        /// </summary>
        /// <param name="match"></param>
        public List(Match match) : 
            base(match)
        {
#if FW1
			ArrayList listItems = new ArrayList();
			for(int capture = 0; capture < match.Groups["Qualifier"].Captures.Count; ++capture)
			{
				listItems.Add(new ListItem(match.Groups["Qualifier"].Captures[capture].Value,
					match.Groups["Title"].Captures[capture].Value));
			} // for

			items = (ListItem[])listItems.ToArray(typeof(ListItem));
#else
            //
            // Loading list items.
            List<ListItem> listItems = new List<ListItem>();
            for(int capture = 0; capture < match.Groups["Qualifier"].Captures.Count; ++capture)
            {
                listItems.Add(new ListItem(match.Groups["Qualifier"].Captures[capture].Value,
                    match.Groups["Title"].Captures[capture].Value));
            } // for

            items = listItems.ToArray();
#endif

        }
    }
}
