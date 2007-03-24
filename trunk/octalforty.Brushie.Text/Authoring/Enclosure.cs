using System;

namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Defines blocks of markup used to surround text authored by <see cref="Textile"/>.
    /// </summary>
    public class Enclosure
    {
        #region Private Member Variables
        private String startBlock;
        private String endBlock;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a <see cref="String"/> with the starting block of markup.
        /// </summary>
        public String StartBlock
        {
            get { return startBlock; }
        }

        /// <summary>
        /// Gets a <see cref="String"/> with the ending block of markup.
        /// </summary>
        public String EndBlock
        {
            get { return endBlock; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Enclosure"/> class with
        /// two blocks of markup.
        /// </summary>
        /// <param name="startBlock">Starting block.</param>
        /// <param name="endBlock">Ending block.</param>
        public Enclosure(String startBlock, String endBlock)
        {
            this.startBlock = startBlock;
            this.endBlock = endBlock;
        }
    }
}
