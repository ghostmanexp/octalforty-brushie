using System;
using System.Collections;
using System.Collections.Generic;

namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Compares two collections, returning a list of the additions and
    /// deletions between them.
    /// </summary>
    public sealed class DiffEngine<T>
    {
        #region Private Member Variables
        private IDataProvider<T> sourceDataProvider;
        private IDataProvider<T> targetDataProvider;
        private Dictionary<int, int?> thresholds;
        private IComparer<T> comparer;
        private Difference pendingCopyDifference;
        private Difference pendingAdditionDifference;
        private Difference pendingDeletionDifference;
        private DifferenceCollection differences = new DifferenceCollection();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="DiffEngine{T}"/> class
        /// with two arrays to compare.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public DiffEngine(T[] source, T[] target) :
            this(new ArrayDataProvider<T>(source), new ArrayDataProvider<T>(target))
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DiffEngine{T}"/> class
        /// with two arrays to compare and an instance of <see cref="IComparer"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="comparer"></param>
        public DiffEngine(T[] source, T[] target, IComparer<T> comparer) :
            this(source, target)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DiffEngine{T}"/> class
        /// with two data providers.
        /// </summary>
        /// <param name="sourceDataProvider"></param>
        /// <param name="targetDataProvider"></param>
        public DiffEngine(IDataProvider<T> sourceDataProvider, IDataProvider<T> targetDataProvider)
        {
            this.sourceDataProvider = sourceDataProvider;
            this.targetDataProvider = targetDataProvider;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DiffEngine{T}"/> class
        /// with two data providers and a reference to the comparer.
        /// </summary>
        /// <param name="sourceDataProvider"></param>
        /// <param name="targetDataProvider"></param>
        /// <param name="comparer"></param>
        public DiffEngine(IDataProvider<T> sourceDataProvider, IDataProvider<T> targetDataProvider, 
            IComparer<T> comparer)
        {
            this.sourceDataProvider = sourceDataProvider;
            this.targetDataProvider = targetDataProvider;

            this.comparer = comparer;
        }

        /// <summary>
        /// Runs the diff and returns a collection of <see cref="Difference"/> objects
        /// which describe the differences.
        /// </summary>
        /// <returns></returns>
        public DifferenceCollection GetDifferences()
        {
            return InternalGetDifferences();
        }

        /// <summary>
        /// Runs the diff.
        /// </summary>
        /// <returns></returns>
        private DifferenceCollection InternalGetDifferences()
        {
            TraverseSequences();
            CommitPendingDifferences();

            return differences;
        }

        /// <summary>
        /// Commits pending difference objects.
        /// </summary>
        /// <remarks>
        /// Note that addition difference is committed first, followed by an
        /// deletion difference.
        /// </remarks>
        private void CommitPendingDifferences()
        {
            if(pendingAdditionDifference != null)
            {
                differences.Add(pendingAdditionDifference);
                pendingAdditionDifference = null;
            } // if

            if(pendingDeletionDifference != null)
            {
                differences.Add(pendingDeletionDifference);
                pendingDeletionDifference = null;
            } // if
        }

        /// <summary>
        /// Traverses the sequences, seeking the longest common subsequences, invoking the methods 
        /// <code>finishedA</code>, <code>finishedB</code>, <code>onANotB</code>, and 
        /// <code>onBNotA</code>.
        /// </summary>
        private void TraverseSequences()
        {
            int targetIndex = 0;
            int sourceIndex = BeginDifferencing(ref targetIndex);
            CompleteDifferencing(sourceIndex, targetIndex);
        }

        /// <summary>
        /// Begins the differencing process.
        /// </summary>
        /// <param name="targetIndex"></param>
        /// <returns></returns>
        private int BeginDifferencing(ref int targetIndex)
        {
            int?[] matches = GetLongestCommonSubsequences();
            int lastMatch = matches.GetLength(0) - 1;

            int sourceIndex;
            for(sourceIndex = 0; sourceIndex <= lastMatch; ++sourceIndex)
            {
                int? targetLineIndex = matches[sourceIndex];

                if(!targetLineIndex.HasValue)
                    SourceNotInTarget(sourceIndex, targetIndex);
                else
                {
                    while(targetIndex < targetLineIndex.Value)
                        InTargetNotInSource(sourceIndex, targetIndex++);

                    Match(sourceIndex, targetIndex++);
                } // else
            } // for

            return sourceIndex;
        }

        /// <summary>
        /// Completes the differencing process.
        /// </summary>
        /// <param name="sourceIndex"></param>
        /// <param name="targetIndex"></param>
        private void CompleteDifferencing(int sourceIndex, int targetIndex)
        {
            int lastSource = sourceDataProvider.Count - 1;
            int lastTarget = targetDataProvider.Count - 1;

            while(sourceIndex <= lastSource || targetIndex <= lastTarget)
            {
                // last A?
                if(sourceIndex == lastSource + 1 && targetIndex <= lastTarget)
                {
                    while(targetIndex <= lastTarget)
                        InTargetNotInSource(sourceIndex, targetIndex++);
                } // if

                // last B?
                if(targetIndex == lastTarget + 1 && sourceIndex <= lastSource)
                {
                    while(sourceIndex <= lastSource)
                        SourceNotInTarget(sourceIndex++, targetIndex);
                } // if

                if(sourceIndex <= lastSource)
                    SourceNotInTarget(sourceIndex++, targetIndex);

                if(targetIndex <= lastTarget)
                    InTargetNotInSource(sourceIndex, targetIndex++);
            }
        }

        /// <summary>
        /// Returns an array of the longest common subsequences.
        /// </summary>
        /// <returns></returns>
        public int?[] GetLongestCommonSubsequences()
        {
            int sourceStart = 0;
            int sourceEnd = sourceDataProvider.Count - 1;

            int targetStart = 0;
            int targetEnd = targetDataProvider.Count - 1;

            Dictionary<int, int> matches = new Dictionary<int, int>();

            while(sourceStart <= sourceEnd && targetStart <= targetEnd && 
                AreEqual(sourceDataProvider[sourceStart], targetDataProvider[targetStart])) 
            {
                matches.Add(sourceStart++, targetStart++);
            } // while

            while(sourceStart <= sourceEnd && targetStart <= targetEnd && 
                AreEqual(sourceDataProvider[sourceEnd], targetDataProvider[targetEnd])) 
            {
                matches.Add(sourceEnd--, targetEnd--);
            } // while

            Dictionary<T, List<int>> targetMatches = new Dictionary<T, List<int>>();

            for(int targetIndex = targetStart; targetIndex <= targetEnd; ++targetIndex) 
            {
                T element = targetDataProvider[targetIndex];
                T key = element;

                List<int> positions;

                if(targetMatches.ContainsKey(key))
                    positions = targetMatches[key];
                else 
                {
                    positions = new List<int>();
                    targetMatches.Add(key, positions);
                } // else
                
                positions.Add(targetIndex);
            } // for

            thresholds = new Dictionary<int, int?>();
            Hashtable links = new Hashtable();

            for(int sourceIndex = sourceStart; sourceIndex <= sourceEnd; ++sourceIndex) 
            {
                T sourceElement = sourceDataProvider[sourceIndex]; // keygen here.

                if(targetMatches.ContainsKey(sourceElement))
                {
                    List<int> positions = targetMatches[sourceElement];

                    int? k = 0;

                    for(int positionIndex = positions.Count - 1; positionIndex >= 0; --positionIndex)
                    {
                        int position = positions[positionIndex];
                        k = Insert(k, position);

                        if(k.HasValue)
                        {
                            Object value = k.Value > 0 ? links[k.Value - 1] : null;
                            links[k] = new Object[] {value, sourceIndex, position};
                        } // if
                    } // for
                } // if
            }

            if(thresholds.Count > 0) 
                ExtractMatches(links, matches);

            return ToArray(matches);
        }

        private void ExtractMatches(Hashtable links, Dictionary<int, int> matches)
        {
            int highestKey = GetHighestKey(thresholds);
            Object[] link = (Object[])links[highestKey];

            while (link != null) 
            {
                int x = (int)link[1];
                int y = (int)link[2];

                matches.Add(x, y);

                link = (Object[])link[0];
            } // while
        }

        /// <summary>
        /// Returns a value which indicates whether <paramref name="value"/> is not zero 
        /// (including if it is not null).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNonzero(int? value)
        {
            return value.HasValue && value.Value != 0;
        }

        /// <summary>
        /// Returns a value which indicates whether the value in the <see cref="thresholds"/>
        /// dictionary with the key <paramref name="thresholdsKey"/> is greater than the 
        /// given <paramref name="value"/>.
        /// </summary>
        /// <param name="thresholdsKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsGreaterThan(int thresholdsKey, int? value)
        {
            int? thresholdValue = thresholds[thresholdsKey];
            return thresholdValue.HasValue && value.HasValue && 
                thresholdValue.Value.CompareTo(value.Value) > 0;
        }

        /// <summary>
        /// Returns a value which indicates whether the value in the <see cref="thresholds"/>
        /// dictionary with the key <paramref name="thresholdsKey"/> is greater than the 
        /// given <paramref name="value"/>.
        /// </summary>
        /// <param name="thresholdsKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsLessThan(int thresholdsKey, int? value)
        {
            int? thresholdValue = thresholds[thresholdsKey];
            return thresholdValue.HasValue && (!value.HasValue || 
                thresholdValue.Value.CompareTo(value.Value) < 0);
        }

        /// <summary>
        /// Returns the value for the greatest key in the <see cref="thresholds"/> dictionary.
        /// </summary>
        /// <returns></returns>
        private int? GetLastThresholdValue()
        {
            return thresholds[GetHighestKey(thresholds)];
        }

        /// <summary>
        /// Inserts <paramref name="value"/> keyed with <paramref name="key"/> into the 
        /// <see cref="thresholds"/> dictionary.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private int? Insert(int? key, int value)
        {
            if(IsNonzero(key) && IsGreaterThan(key.Value, value) && IsLessThan(key.Value - 1, value))
                thresholds[key.Value] = value;
            else
            {
                int highestThresholdKey = -1;

                if(IsNonzero(key))
                    highestThresholdKey = key.Value;
                else if(thresholds.Count > 0)
                {
                    highestThresholdKey = GetHighestKey(thresholds);
                } // else if

                // off the end?
                if(highestThresholdKey == -1 || value.CompareTo(GetLastThresholdValue()) > 0)
                {
                    Append(value);
                    key = highestThresholdKey + 1;
                }
                else
                {
                    // binary search for insertion point:
                    int lowestThresholdKey = 0;

                    while(lowestThresholdKey <= highestThresholdKey)
                    {
                        int index = (highestThresholdKey + lowestThresholdKey) / 2;
                        int? thresholdValue = thresholds[index];
                        int comparisonResult = value.CompareTo(thresholdValue);

                        if(comparisonResult == 0)
                            return null;
                        else if(comparisonResult > 0)
                            lowestThresholdKey = index + 1;
                        else
                            highestThresholdKey = index - 1;
                    }

                    thresholds[lowestThresholdKey] = value;
                    key = lowestThresholdKey;
                } // else
            } // else

            return key;
        }

        /// <summary>
        /// Adds the given <paramref name="value"/> to the end of the <see cref="thresholds"/> map, 
        /// that is, with the greatest index/key.
        /// </summary>
        /// <param name="value"></param>
        private void Append(int value)
        {
            int? addIdx;

            if(thresholds.Count == 0)
                addIdx = 0;
            else
            {
                int lastKey = GetHighestKey(thresholds);
                addIdx = lastKey + 1;
            } // else

            thresholds.Add(addIdx.Value, value);
        }


        /// <summary>
        /// Compares the two objects, using the comparer provided, if any.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool AreEqual(T x, T y)
        {
            return comparer == null ? 
                x.Equals(y) : 
                comparer.Compare(x, y) == 0;
        }

        /// <summary>
        /// Converts <paramref name="dictionary"/> to array.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private static int?[] ToArray(Dictionary<int, int> dictionary)
        {
            int size = dictionary.Count == 0 ? 
                0 : 1 + GetHighestKey(dictionary);

            int?[] array = new int?[size];

            foreach(int key in dictionary.Keys)
            {
                array[key] = dictionary[key];
            } // foreach

            return array;
        }

        /// <summary>
        /// Gets the largest key in the <see cref="thresholds"/> dictionary.
        /// </summary>
        /// <returns></returns>
        private static TKey GetHighestKey<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            if(dictionary.Count == 0)
                return default(TKey);

            //
            // Searching for the largest value.
            TKey highestKey = default(TKey);
            foreach(TKey key in dictionary.Keys)
            {
                if(!(key is IComparable))
                    return highestKey;

                if((key as IComparable).CompareTo(highestKey) > 0)
                    highestKey = key;
            } // foreach

            return highestKey;
        }

        /// <summary>
        /// Invoked for elements in <see cref="sourceDataProvider"/> and not in 
        /// <see cref="targetDataProvider"/>.
        /// </summary>
        /// <param name="sourceIndex"></param>
        /// <param name="targetIndex"></param>
        private void SourceNotInTarget(int sourceIndex, int targetIndex)
        {
            CommitMatch();

            if(pendingDeletionDifference == null)
            {
                pendingDeletionDifference =
                    Difference.CreateDeletion(new Range<int>(sourceIndex, sourceIndex));
            } // if
            else
            {
                pendingDeletionDifference.Deletion =
                    new Range<int>(Math.Min(pendingDeletionDifference.Deletion.Start, sourceIndex),
                        Math.Max(pendingDeletionDifference.Deletion.Start, sourceIndex));
            } // else
        }

        private void CommitMatch()
        {
            if(pendingCopyDifference != null)
            {
                differences.Add(pendingCopyDifference);
                pendingCopyDifference = null;
            } // if
        }

        /// <summary>
        /// Invoked for elements in <see cref="targetDataProvider"/> and not in 
        /// <see cref="sourceDataProvider"/>.
        /// </summary>
        /// <param name="sourceIndex"></param>
        /// <param name="targetIndex"></param>
        private void InTargetNotInSource(int sourceIndex, int targetIndex)
        {
            CommitMatch();

            if(pendingAdditionDifference == null)
            {
                pendingAdditionDifference =
                    Difference.CreateAddition(new Range<int>(targetIndex, targetIndex));
            } // if
            else
            {
                pendingAdditionDifference.Addition =
                    new Range<int>(Math.Min(pendingAdditionDifference.Addition.Start, targetIndex),
                        Math.Max(pendingAdditionDifference.Addition.Start, targetIndex));
            } // else
        }
        

        /// <summary>
        /// Invoked for elements matching in <see cref="sourceDataProvider"/> and 
        /// <see cref="targetDataProvider"/>.
        /// </summary>
        /// <param name="sourceIndex"></param>
        /// <param name="targetIndex"></param>
        private void Match(int sourceIndex, int targetIndex)
        {
            if(pendingCopyDifference == null)
            {
                pendingCopyDifference = Difference.CreateCopy(sourceIndex, sourceIndex);
            } // if
            else
            {
                pendingCopyDifference.Copy =
                    new Range<int>(Math.Min(pendingCopyDifference.Copy.Start, sourceIndex),
                        Math.Max(pendingCopyDifference.Copy.Start, sourceIndex));
            } // else

            CommitPendingDifferences();
        }
    }
}
