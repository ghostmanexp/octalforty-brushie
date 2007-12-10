namespace octalforty.Brushie.Web.UI
{
    /// <summary>
    /// Defines the type of the chart.
    /// </summary>
    public enum GoogleChartType
    {
        /// <summary>
        /// A line chart, data points are spaced evenly along the x-axis.
        /// </summary>
        [GoogleChartTypeName("lc")]
        LineChart = 0,

        /// <summary>
        /// Provide a pair of data sets for each line you wish to draw, the first data set of each pair 
        /// specifies the x-axis coordinates, the second the y-axis coordinates.
        /// </summary>
        [GoogleChartTypeName("lxy")]
        XYLineChart = 1,

        /// <summary>
        /// Horizontal bar chart.
        /// </summary>
        [GoogleChartTypeName("bhs")]
        HorizontalBarChart = 2,

        /// <summary>
        /// Verical bar chart.
        /// </summary>
        [GoogleChartTypeName("bvs")]
        VerticalBarChart = 3,

        /// <summary>
        /// Horizontal bar chart; multiple data sets are grouped.
        /// </summary>
        [GoogleChartTypeName("bhg")]
        GroupedHorizontalBarChart = 4,

        /// <summary>
        /// Verical bar chart; multiple data sets are grouped.
        /// </summary>
        [GoogleChartTypeName("bvg")]
        GroupedVerticalBarChart = 5,

        /// <summary>
        /// Two dimensional pie chart.
        /// </summary>
        [GoogleChartTypeName("p")]
        PieChart = 6,

        /// <summary>
        /// Three dimensional pie chart.
        /// </summary>
        [GoogleChartTypeName("p3")]
        PieChart3D = 7,

        /// <summary>
        /// Vienn diagram.
        /// </summary>
        [GoogleChartTypeName("v")]
        VennDiagram = 8,

        /// <summary>
        /// Scatter plot.
        /// </summary>
        [GoogleChartTypeName("s")]
        ScatterPlot = 9
    }
}
