using System;
using System.Collections;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Displays a Google Chart on the web page.
    /// </summary>
    public class GoogleChart : WebControl
    {
        #region Private Member Variables
        private GoogleChartDataEncoder encoder;
        private GoogleChartType chartType;
        private object dataSource;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a <see cref="GoogleChartDataEncoder"/>, which defines the encoder.
        /// </summary>
        public GoogleChartDataEncoder Encoder
        {
            get { return encoder; }
            set { encoder = value; }
        }

        /// <summary>
        /// Gets or sets a <see cref="GoogleChartType"/>, which defines the type of the chart.
        /// </summary>
        public GoogleChartType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        /// <summary>
        /// Gets or sets a reference to the data source of this <see cref="GoogleChart"/>.
        /// </summary>
        public object DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleChart"/> class.
        /// </summary>
        public GoogleChart()
        {
        }

        /// <summary>
        /// Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">The <see cref="System.Web.UI.HtmlTextWriter" /> 
        /// object that receives the control content. </param>
        protected override void Render(HtmlTextWriter writer)
        {
            if(Width.Type != UnitType.Pixel)
                throw new ArgumentOutOfRangeException("Width");

            if(Height.Type != UnitType.Pixel)
                throw new ArgumentOutOfRangeException("Height");

            GoogleChartUrlBuilder googleChartUrlBuilder = 
                new GoogleChartUrlBuilder(new Size((int)Width.Value, (int)Height.Value), ChartType, 
                GetGoogleChartDataEncoder());

            //
            // Binding
            if(DataSource != null)
            {
                if(DataSource is IEnumerable)
                    DataBindEnumerable(googleChartUrlBuilder, DataSource as IEnumerable);
            } // if

            writer.AddAttribute(HtmlTextWriterAttribute.Src, googleChartUrlBuilder.BuildUrl());
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
        }

        private static void DataBindEnumerable(GoogleChartUrlBuilder googleChartUrlBuilder, IEnumerable enumerable)
        {
            googleChartUrlBuilder.AddDataSet(enumerable);
        }

        private IGoogleChartDataEncoder GetGoogleChartDataEncoder()
        {
            switch(Encoder)
            {
                case GoogleChartDataEncoder.Simple:
                    return new SimpleGoogleChartDataEncoder();
                case GoogleChartDataEncoder.Text:
                    throw new ArgumentOutOfRangeException("Encoder");
                case GoogleChartDataEncoder.Extended:
                    throw new ArgumentOutOfRangeException("Encoder");
                default:
                    throw new ArgumentOutOfRangeException("Encoder");
            }
        }
    }
}
