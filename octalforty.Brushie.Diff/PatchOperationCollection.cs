using System.Collections.ObjectModel;

namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Represents a collection of <see cref="PatchOperation"/> objects.
    /// </summary>
    public class PatchOperationCollection : Collection<PatchOperation>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PatchOperationCollection"/> class.
        /// </summary>
        public PatchOperationCollection()
        {
        }
    }
}
