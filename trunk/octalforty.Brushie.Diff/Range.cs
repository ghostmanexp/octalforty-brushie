namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Defines a simple range between two points, <see cref="Start"/> and <see cref="End"/>.
    /// </summary>
    public struct Range<T>
    {
        #region Private Member Variables
        private T start;
        private T end;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the starting point of the range.
        /// </summary>
        public T Start
        {
            get { return start; }
        }

        /// <summary>
        /// Gets the ending point of the range.
        /// </summary>
        public T End
        {
            get { return end; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Range{T}"/> structure with the
        /// given starting and end ending points.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">Ending point.</param>
        public Range(T start, T end)
        {
            this.start = start;
            this.end = end;
        }

        #region Object Members
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the 
        /// current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// <see langword="true" /> if the specified object is equal to the current Object; 
        /// otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is Range<T>))
                return false;

            Range<T> range = (Range<T>)obj;
            return Equals(Start, range.Start) && Equals(End, range.End);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// <see cref="GetHashCode"/> is suitable for use in hashing algorithms and data structures 
        /// like a hash table.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Start.GetHashCode() + 29 * End.GetHashCode();
        }
        #endregion
    }
}
