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
            
            Assert.AreEqual("s=Hey&intField=122&iaf=1,23,456", queryString);
        }

        [Test()]
        public void Compile2()
        {
            QueryStringCompiler queryStringCompiler = new QueryStringCompiler();
            QueryStringContainer queryStringContainer = new QueryStringContainer("Hey", 122, new int[] { 1, 23, 456 });
            queryStringContainer.DateField = new DateTime(2007, 4, 8);
            queryStringContainer.DateTimeField = new DateTime(2007, 4, 8, 12, 34, 56);

            string queryString = queryStringCompiler.Compile(queryStringContainer);

            Assert.AreEqual("s=Hey&intField=122&iaf=1,23,456&df=20070408&dtf=20070408123456", queryString);
        }
    }
}
