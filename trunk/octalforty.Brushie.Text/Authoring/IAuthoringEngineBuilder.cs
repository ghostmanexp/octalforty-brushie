namespace octalforty.Brushie.Text.Authoring
{
    /// <summary>
    /// Constructs <see cref="IAuthoringEngine"/> by adding <see cref="IInlineElementParser"/>s and 
    /// <see cref="IBlockElementParser"/>s in appropriate order.
    /// </summary>
    public interface IAuthoringEngineBuilder
    {
        /// <summary>
        /// Creates an instance of <see cref="IAuthoringEngine"/>.
        /// </summary>
        /// <returns></returns>
        IAuthoringEngine CreateAuthoringEngine();
    }
}
