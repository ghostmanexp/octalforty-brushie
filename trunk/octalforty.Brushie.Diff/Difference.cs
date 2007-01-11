using System;

namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Defines a single difference returned by the <see cref="DiffEngine{T}"/>.
    /// </summary>
    public class Difference
    {
        #region Private Member Variables
        private DifferenceType type;
        private Range<int> addition;
        private Range<int> deletion;
        private Range<int> copy;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="DifferenceType"/> which defines the
        /// type of the current object.
        /// </summary>
        public DifferenceType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the <see cref="Range{T}"/>, which defines the addition range for
        /// the current difference.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>DifferenceType.Addition</c>.
        /// </exception>
        public Range<int> Addition
        {
            get
            {
                CheckType(DifferenceType.Addition);
                return addition;
            }
            set
            {
                CheckType(DifferenceType.Addition);
                addition = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Range{T}"/>, which defines the deletion range for
        /// the current difference.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>DifferenceType.Deletion</c>.
        /// </exception>
        public Range<int> Deletion
        {
            get
            {
                CheckType(DifferenceType.Deletion);
                return deletion;
            }
            set
            {
                CheckType(DifferenceType.Deletion);
                deletion = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Range{T}"/>, which defines the copy range for
        /// the current difference.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>DifferenceType.Copy</c>.
        /// </exception>
        public Range<int> Copy
        {
            get
            {
                CheckType(DifferenceType.Copy);
                return copy;
            }
            set
            {
                CheckType(DifferenceType.Copy);
                copy = value;
            }
        }

        /// <summary>
        /// Gets a reference to the <see cref="Range{T}"/> valid at the current point.
        /// </summary>
        public Range<int> Range
        {
            get
            {
                if(Type == DifferenceType.Addition)
                    return Addition;
                else if(Type == DifferenceType.Copy)
                    return Copy;
                else
                    return Deletion;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Difference"/> class with
        /// the given type and addition and deletion ranges.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="addition"></param>
        /// <param name="deletion"></param>
        /// <param name="copy"></param>
        private Difference(DifferenceType type, Range<int> addition, Range<int> deletion, 
            Range<int> copy)
        {
            this.type = type;
            this.addition = addition;
            this.deletion = deletion;
            this.copy = copy;
        }

        /// <summary>
        /// Ensures that <see cref="Type"/> is equal to <paramref name="requiredType"/>.
        /// </summary>
        /// <param name="requiredType"></param>
        private void CheckType(DifferenceType requiredType)
        {
            if(Type != requiredType)
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// deletion for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Difference CreateDeletion(Range<int> range)
        {
            return new Difference(DifferenceType.Deletion, new Range<int>(), range, new Range<int>());
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// deletion for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Difference CreateDeletion(int start, int end)
        {
            return CreateDeletion(new Range<int>(start, end));
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// addition for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Difference CreateAddition(Range<int> range)
        {
            return new Difference(DifferenceType.Addition, range, new Range<int>(), new Range<int>());
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// addition for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Difference CreateAddition(int start, int end)
        {
            return CreateAddition(new Range<int>(start, end));
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// copy for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Difference CreateCopy(Range<int> range)
        {
            return new Difference(DifferenceType.Copy, new Range<int>(), new Range<int>(), range);
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// copy for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Difference CreateCopy(int start, int end)
        {
            return CreateCopy(new Range<int>(start, end));
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
            if(this == obj) 
                return true;

            Difference difference = obj as Difference;
            if(difference == null) 
                return false;

            if(!Equals(Type, difference.Type)) 
                return false;

            if(Type == DifferenceType.Addition)
                return Equals(Addition, difference.Addition);

            if(Type == DifferenceType.Deletion)
                return Equals(Deletion, difference.Deletion);

            if(Type == DifferenceType.Copy)
                return Equals(Copy, difference.Copy);

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// <see cref="GetHashCode"/> is suitable for use in hashing algorithms and data structures 
        /// like a hash table.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int result = Type.GetHashCode();

            if(Type == DifferenceType.Addition)
                result = 29 * result + Addition.GetHashCode();
            
            if(Type == DifferenceType.Deletion)
                result = 29 * result + Deletion.GetHashCode();

            if(Type == DifferenceType.Copy)
                result = 29 * result + Copy.GetHashCode();

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}: [{1},{2}]", Type, Range.Start, Range.End);
        }
        #endregion
    }
}
