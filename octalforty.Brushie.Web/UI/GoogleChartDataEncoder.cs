namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Defines the type of the Google Chart Data Encoder.
    /// </summary>
    public enum GoogleChartDataEncoder
    {
        /// <summary>
        /// Simple encoding has a resolution of 62 different values. Allowing five pixels per data point, 
        /// this is sufficient for line and bar charts up to about 300 pixels. Simple encoding is suitable 
        /// for all other types of chart regardless of size.
        /// <seealso cref="SimpleGoogleChartDataEncoder"/>
        /// </summary>
        Simple = 0,

        /// <summary>
        /// Text encoding has a resolution of 1,000 different values, using floating point numbers between 0.0 
        /// and 100.0. Allowing five pixels per data point, integers (1.0, 2.0, and so on) are sufficient for line 
        /// and bar charts up to about 500 pixels. Include a single decimal place (35.7 for example) if you require 
        /// higher resolution. Text encoding is suitable for all other types of chart regardless of size.
        /// </summary>
        Text = 1,

        /// <summary>
        /// Extended encoding has a resolution of 4,096 different values and is best used for large charts 
        /// where a large data range is required.
        /// </summary>
        Extended = 2
    }
}
