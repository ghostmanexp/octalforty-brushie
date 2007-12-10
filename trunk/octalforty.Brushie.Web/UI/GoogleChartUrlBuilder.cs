using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Builds well-formed URLs for Google Charts.
    /// </summary>
    public class GoogleChartUrlBuilder
    {
        #region Private Member Variables
        private Size chartSize;
        private GoogleChartType chartType;
        private IGoogleChartDataEncoder encoder;
        private List<string> dataSets = new List<string>();
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleChartUrlBuilder"/> class.
        /// </summary>
        /// <param name="chartSize"></param>
        /// <param name="chartType"></param>
        /// <param name="encoder"></param>
        public GoogleChartUrlBuilder(Size chartSize, GoogleChartType chartType, IGoogleChartDataEncoder encoder)
        {
            this.chartSize = chartSize;
            this.chartType = chartType;
            this.encoder = encoder;
        }

        /// <summary>
        /// Adds a dataset to the Google Chart datasource.
        /// </summary>
        /// <param name="enumerable"></param>
        public void AddDataSet(IEnumerable enumerable)
        {
            dataSets.Add(encoder.Encode(enumerable));
        }

        /// <summary>
        /// Builds an URL.
        /// </summary>
        /// <returns></returns>
        public string BuildUrl()
        {
            StringBuilder uriBuilder = new StringBuilder("http://chart.apis.google.com/chart?");

            uriBuilder.AppendFormat("chs={0}x{1}", chartSize.Width, chartSize.Height);
            uriBuilder.AppendFormat("&cht={0}", GetChartTypeName());
            uriBuilder.AppendFormat("&chd={0}:{1}", 
                encoder.Prefix, string.Join(encoder.DataSetSeparator, dataSets.ToArray()));

            return uriBuilder.ToString();
        }

        private string GetChartTypeName()
        {
            FieldInfo fieldInfo = typeof(GoogleChartType).GetField(chartType.ToString());
            object[] attributes = 
                fieldInfo.GetCustomAttributes(typeof(GoogleChartTypeNameAttribute), false);

            return ((GoogleChartTypeNameAttribute)attributes[0]).Name;
        }
    }
}
