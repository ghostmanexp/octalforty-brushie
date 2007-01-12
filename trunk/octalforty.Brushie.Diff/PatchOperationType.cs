namespace octalforty.Brushie.Diff
{
    /// <summary>
    /// Defines the type of the <see cref="PatchOperation"/>.
    /// </summary>
    public enum PatchOperationType
    {
        /// <summary>
        /// Type is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Simple copy content.
        /// </summary>
        Copy = 1,

        /// <summary>
        /// Addition.
        /// </summary>
        Addition = 2,

        /// <summary>
        /// Deletion.
        /// </summary>
        Deletion = 3
    }
}
