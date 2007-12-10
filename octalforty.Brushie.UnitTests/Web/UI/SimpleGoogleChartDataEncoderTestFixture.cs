using System;

using NUnit.Framework;

using octalforty.Brushie.Web.UI;

namespace octalforty.Brushie.UnitTests.Web.UI
{
    /// <summary>
    /// <see cref="SimpleGoogleChartDataEncoder"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class SimpleGoogleChartDataEncoderTestFixture
    {
        [Test()]
        public void Encode()
        {
            IGoogleChartDataEncoder googleChartDataEncoder = new SimpleGoogleChartDataEncoder();
            Assert.AreEqual("BCx4U", googleChartDataEncoder.Encode(new byte[] { 1, 2, 49, 56, 20 }));
        }

        [Test()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EncodeArgumentOutOfRange()
        {
            IGoogleChartDataEncoder googleChartDataEncoder = new SimpleGoogleChartDataEncoder();
            googleChartDataEncoder.Encode(new byte[] { 1, 2, 49, 56, 20, 250 });
        }
    }
}
