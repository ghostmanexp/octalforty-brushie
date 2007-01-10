using System.Collections.ObjectModel;

namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Represents a collection of <see cref="Difference"/> objects.
    /// </summary>
    public class DifferenceCollection : Collection<Difference>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DifferenceCollection"/> class.
        /// </summary>
        public DifferenceCollection()
        {
        }
    }
}
