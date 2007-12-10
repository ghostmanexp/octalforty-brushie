using System.Drawing;

using NUnit.Framework;

using octalforty.Brushie.Web.UI;

namespace octalforty.Brushie.UnitTests.Web.UI
{
    /// <summary>
    /// <see cref="GoogleChartUrlBuilder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class GoogleChartUrlBuilderTestFixture
    {
        [Test()]
        public void BuildUrl()
        {
            GoogleChartUrlBuilder googleChartUrlBuilder = new GoogleChartUrlBuilder(
                new Size(300, 200), GoogleChartType.LineChart, new SimpleGoogleChartDataEncoder());
            googleChartUrlBuilder.AddDataSet(new byte[] { 1, 2, 49, 56, 20,  });

            Assert.AreEqual("http://chart.apis.google.com/chart?chs=300x200&cht=lc&chd=s:BCx4U", 
                googleChartUrlBuilder.BuildUrl());
        }
    }
}
