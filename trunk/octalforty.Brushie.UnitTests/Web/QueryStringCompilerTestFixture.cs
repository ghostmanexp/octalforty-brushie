using System;

using NUnit.Framework;

using octalforty.Brushie.Web;

namespace octalforty.Brushie.UnitTests.Web
{
    /// <summary>
    /// <see cref="QueryStringCompiler"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class QueryStringCompilerTestFixture
    {
        [Test()]
        public void Compile()
        {
            QueryStringCompiler queryStringCompiler = new QueryStringCompiler();
            QueryStringContainer queryStringContainer = new QueryStringContainer("Hey", 122, new int[] { 1, 23, 456 });

            string queryString = queryStringCompiler.Compile(queryStringContainer);
            
            Assert.AreEqual("s=Hey&intField=122&iaf=1,23,456&b=0", queryString);
        }

        [Test()]
        public void Compile2()
        {
            QueryStringCompiler queryStringCompiler = new QueryStringCompiler();
            QueryStringContainer queryStringContainer = new QueryStringContainer("Hey", 122, new int[] { 1, 23, 456 });
            queryStringContainer.DateField = new DateTime(2007, 4, 8);
            queryStringContainer.DateTimeField = new DateTime(2007, 4, 8, 12, 34, 56);
            queryStringContainer.Strings.Add("Hi");
            queryStringContainer.Strings.Add("There");
            queryStringContainer.BoolField = true;

            string queryString = queryStringCompiler.Compile(queryStringContainer);

            Assert.AreEqual("s=Hey&intField=122&iaf=1,23,456&df=20070408&dtf=20070408123456&sts=Hi,There&b=1", queryString);
        }
    }
}
