using System.Collections;

namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Defines a contract for the encoder of Google Chart datapoints.
    /// </summary>
    public interface IGoogleChartDataEncoder
    {
        /// <summary>
        /// Gets a string which is used as a prefix in the <c>chd</c> parameter.
        /// </summary>
        string Prefix
        { get; }

        /// <summary>
        /// Gets a string which is used to separate multiple sets of data.
        /// </summary>
        string DataSetSeparator
        { get; }

        /// <summary>
        /// Encodes a given <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        string Encode(IEnumerable enumerable);
    }
}
