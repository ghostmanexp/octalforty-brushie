using System.Collections;
#if FW2
using System.Collections.Generic;
#endif
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Web;

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
#if FW2
        private List<string> dataSets = new List<string>();
#else
		private ArrayList dataSets = new ArrayList();
#endif
        private StringCollection pieChartLabels = new StringCollection();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a reference to the <see cref="StringCollection"/>, which contains
        /// labels for the pie chart.
        /// </summary>
        public StringCollection PieChartLabels
        {
            get { return pieChartLabels; }
        }
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
            uriBuilder.AppendFormat("&chd={0}:{1}", encoder.Prefix, 
#if FW2
                string.Join(encoder.DataSetSeparator, dataSets.ToArray()));
#else
				string.Join(encoder.DataSetSeparator, (string[])dataSets.ToArray(typeof(string))));
#endif

            //
            // Pie chart labels
            if((chartType == GoogleChartType.PieChart || chartType == GoogleChartType.PieChart3D) && PieChartLabels.Count > 0)
                uriBuilder.AppendFormat("&chl={0}", string.Join("|", GetPieChartLabels()));

            return uriBuilder.ToString();
        }

        private string[] GetPieChartLabels()
        {
#if FW2
            List<string> labels = new List<string>();
#else
			ArrayList labels = new ArrayList();
#endif
            foreach(string pieChartLabel in pieChartLabels)
                labels.Add(HttpUtility.UrlEncode(pieChartLabel));

#if FW2
            return labels.ToArray();
#else
			return (string[])labels.ToArray(typeof(string));
#endif
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
