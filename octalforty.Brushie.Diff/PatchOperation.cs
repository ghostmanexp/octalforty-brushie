using System;

namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Defines a single patch operation returned by the <see cref="DiffEngine{T}"/>.
    /// </summary>
    public class PatchOperation
    {
        #region Private Member Variables
        private PatchOperationType type;
        private Range<int> addition;
        private Range<int> deletion;
        private Range<int> copy;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="PatchOperationType"/> which defines the
        /// type of the current object.
        /// </summary>
        public PatchOperationType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the <see cref="octalforty.Brushie.Diff.Range{T}"/>, which defines the addition range for
        /// the current difference. Please note that this property defines
        /// a range within the target data source.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>PatchOperationType.Addition</c>.
        /// </exception>
        public Range<int> Addition
        {
            get
            {
                CheckType(PatchOperationType.Addition);
                return addition;
            }
            set
            {
                CheckType(PatchOperationType.Addition);
                addition = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="octalforty.Brushie.Diff.Range{T}"/>, which defines the deletion range for
        /// the current difference.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>PatchOperationType.Deletion</c>.
        /// </exception>
        public Range<int> Deletion
        {
            get
            {
                CheckType(PatchOperationType.Deletion);
                return deletion;
            }
            set
            {
                CheckType(PatchOperationType.Deletion);
                deletion = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="octalforty.Brushie.Diff.Range{T}"/>, which defines the copy range for
        /// the current difference.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When <see cref="Type"/> is not equal to <c>PatchOperationType.Copy</c>.
        /// </exception>
        public Range<int> Copy
        {
            get
            {
                CheckType(PatchOperationType.Copy);
                return copy;
            }
            set
            {
                CheckType(PatchOperationType.Copy);
                copy = value;
            }
        }

        /// <summary>
        /// Gets a reference to the <see cref="octalforty.Brushie.Diff.Range{T}"/> valid at the current point.
        /// </summary>
        public Range<int> Range
        {
            get
            {
                if(Type == PatchOperationType.Addition)
                    return Addition;
                else if(Type == PatchOperationType.Copy)
                    return Copy;
                else
                    return Deletion;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="PatchOperation"/> class with
        /// the given type and addition and deletion ranges.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="addition"></param>
        /// <param name="deletion"></param>
        /// <param name="copy"></param>
        private PatchOperation(PatchOperationType type, Range<int> addition, Range<int> deletion, 
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
        private void CheckType(PatchOperationType requiredType)
        {
            if(Type != requiredType)
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// deletion for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static PatchOperation CreateDeletion(Range<int> range)
        {
            return new PatchOperation(PatchOperationType.Deletion, new Range<int>(), range, new Range<int>());
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// deletion for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static PatchOperation CreateDeletion(int start, int end)
        {
            return CreateDeletion(new Range<int>(start, end));
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// addition for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static PatchOperation CreateAddition(Range<int> range)
        {
            return new PatchOperation(PatchOperationType.Addition, range, new Range<int>(), new Range<int>());
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// addition for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static PatchOperation CreateAddition(int start, int end)
        {
            return CreateAddition(new Range<int>(start, end));
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// copy for the <paramref name="range"/>.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static PatchOperation CreateCopy(Range<int> range)
        {
            return new PatchOperation(PatchOperationType.Copy, new Range<int>(), new Range<int>(), range);
        }

        /// <summary>
        /// Creates an instance of <see cref="PatchOperation"/> which describes a single
        /// copy for the range from <paramref name="start"/> to <paramref name="end"/>.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static PatchOperation CreateCopy(int start, int end)
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

            PatchOperation difference = obj as PatchOperation;
            if(difference == null) 
                return false;

            if(!Equals(Type, difference.Type)) 
                return false;

            if(Type == PatchOperationType.Addition)
                return Equals(Addition, difference.Addition);

            if(Type == PatchOperationType.Deletion)
                return Equals(Deletion, difference.Deletion);

            if(Type == PatchOperationType.Copy)
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

            if(Type == PatchOperationType.Addition)
                result = 29 * result + Addition.GetHashCode();
            
            if(Type == PatchOperationType.Deletion)
                result = 29 * result + Deletion.GetHashCode();

            if(Type == PatchOperationType.Copy)
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
