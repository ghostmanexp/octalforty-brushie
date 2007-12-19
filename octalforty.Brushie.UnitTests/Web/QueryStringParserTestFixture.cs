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
            queryStringFields.Add("sts", "Hi,There");
            queryStringFields.Add("integers", "1,2,3,4,5");
            queryStringFields.Add("b", "1");

            queryStringParser.Parse(queryStringFields, queryStringContainer);

            Assert.AreEqual("Hey", queryStringContainer.StringField);
            Assert.AreEqual(122, queryStringContainer.IntField);
            Assert.AreEqual(new int[] { 1, 23, 456 }, queryStringContainer.IntArrayField);
            Assert.AreEqual(new DateTime(2007, 4, 8), queryStringContainer.DateField);
            Assert.AreEqual(new DateTime(2007, 4, 8, 12, 34, 56), queryStringContainer.DateTimeField);

            Assert.AreEqual("Hi", queryStringContainer.Strings[0]);
            Assert.AreEqual("There", queryStringContainer.Strings[1]);

            Assert.AreEqual(1, queryStringContainer.Ints[0]);
            Assert.AreEqual(2, queryStringContainer.Ints[1]);
            Assert.AreEqual(3, queryStringContainer.Ints[2]);
            Assert.AreEqual(4, queryStringContainer.Ints[3]);
            Assert.AreEqual(5, queryStringContainer.Ints[4]);

            Assert.IsTrue(queryStringContainer.BoolField);
        }
    }
}
