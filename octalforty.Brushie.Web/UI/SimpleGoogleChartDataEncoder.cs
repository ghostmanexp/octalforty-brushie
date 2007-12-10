using System;
using System.Collections;
using System.Text;

namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Simple encoding has a resolution of 62 different values. Allowing five pixels per data point, 
    /// this is sufficient for line and bar charts up to about 300 pixels. Simple encoding is 
    /// suitable for all other types of chart regardless of size.
    /// </summary>
    public class SimpleGoogleChartDataEncoder : IGoogleChartDataEncoder
    {
        #region Private Constants
        private const string Encoding = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleGoogleChartDataEncoder"/>.
        /// </summary>
        public SimpleGoogleChartDataEncoder()
        {
        }

        #region IGoogleChartDataEncoder Members
        /// <summary>
        /// Gets a string which is used as a prefix in the <c>chd</c> parameter.
        /// </summary>
        public string Prefix
        {
            get { return "s"; }
        }

        /// <summary>
        /// Gets a string which is used to separate multiple sets of data.
        /// </summary>
        public string DataSetSeparator
        {
            get { return ","; }
        }

        /// <summary>
        /// Encodes a given <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public string Encode(IEnumerable enumerable)
        {
            StringBuilder encodedString = new StringBuilder();

            try
            {
                foreach(object dataPoint in enumerable)
                {
                    byte value = Convert.ToByte(dataPoint);
                    if(value > Encoding.Length)
                        throw new ArgumentOutOfRangeException();

                    encodedString.Append(Encoding[value]);
                } // foreach

                return encodedString.ToString();
            } // try

            catch(Exception e)
            {
                throw;
            } // catch
        }
        #endregion
    }
}
