using System;
using System.Collections.Specialized;

using NUnit.Framework;

using octalforty.Brushie.Web;

namespace octalforty.Brushie.UnitTests.Web
{
    /// <summary>
    /// <see cref="QueryStringParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class QueryStringParserTestFixture
    {
        [Test()]
        public void Parse()
        {
            QueryStringContainer queryStringContainer = new QueryStringContainer();
            QueryStringParser queryStringParser = new QueryStringParser();

            NameValueCollection queryStringFields = new NameValueCollection();
            queryStringFields.Add("s", "Hey");
            queryStringFields.Add("intField", "122");
            queryStringFields.Add("iaf", "1,23,456");
            queryStringFields.Add("df", "20070408");
            queryStringFields.Add("dtf", "20070408123456");

            queryStringParser.Parse(queryStringFields, queryStringContainer);

            Assert.AreEqual("Hey", queryStringContainer.StringField);
            Assert.AreEqual(122, queryStringContainer.IntField);
            Assert.AreEqual(new int[] { 1, 23, 456 }, queryStringContainer.IntArrayField);
            Assert.AreEqual(new DateTime(2007, 4, 8), queryStringContainer.DateField);
            Assert.AreEqual(new DateTime(2007, 4, 8, 12, 34, 56), queryStringContainer.DateTimeField);
        }
    }
}
