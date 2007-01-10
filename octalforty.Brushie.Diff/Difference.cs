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
        private Range<long> addition;
        private Range<long> deletion;
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
        public Range<long> Addition
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
        public Range<long> Deletion
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
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="Difference"/> class with
        /// the given type and addition and deletion ranges.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="addition"></param>
        /// <param name="deletion"></param>
        private Difference(DifferenceType type, Range<long> addition, Range<long> deletion)
        {
            this.type = type;
            this.addition = addition;
            this.deletion = deletion;
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
        public static Difference CreateDeletion(Range<long> range)
        {
            return new Difference(DifferenceType.Deletion, new Range<long>(), range);
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// deletion for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Difference CreateDeletion(long start, long end)
        {
            return CreateDeletion(new Range<long>(start, end));
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// addition for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Difference CreateAddition(Range<long> range)
        {
            return new Difference(DifferenceType.Addition, range, new Range<long>());
        }

        /// <summary>
        /// Creates an instance of <see cref="Difference"/> which describes a single
        /// addition for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Difference CreateAddition(long start, long end)
        {
            return CreateAddition(new Range<long>(start, end));
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
                result = 29 * result + addition.GetHashCode();
            
            if(Type == DifferenceType.Deletion)
                result = 29 * result + deletion.GetHashCode();

            return result;
        }
        #endregion
    }
}
