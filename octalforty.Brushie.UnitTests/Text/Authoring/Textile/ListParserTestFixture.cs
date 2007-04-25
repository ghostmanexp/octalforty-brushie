using NUnit.Framework;

using octalforty.Brushie.Text.Authoring;
using octalforty.Brushie.Text.Authoring.Dom;
using octalforty.Brushie.Text.Authoring.Textile;

namespace octalforty.Brushie.UnitTests.Text.Authoring.Textile
{
    /// <summary>
    /// <see cref="ListParser"/> unit tests.
    /// </summary>
    [TestFixture()]
    public class ListParserTestFixture
    {
        [Test()]
        public void ParseMultilevelList()
        {
            const string MultilevelListMarkup = "# one\r\n## aye\r\n## bee\r\n## see\r\n# two\r\n## x\r\n## y\r\n# three\r\n";

            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new ListParser();

            blockElementParser.Parse(new TextileAuthoringEngine(), document, MultilevelListMarkup);

            //
            // Root ordered list
            OrderedList rootOrderedList = document.ChildElements[0] as OrderedList;
            
            Assert.IsNotNull(rootOrderedList);
            Assert.AreEqual(3, rootOrderedList.ChildElements.Count);

            //
            // First list item, which contains two child items - TextBlock and OrderedList
            ListItem item = rootOrderedList.ChildElements[0] as ListItem;
            
            Assert.IsNotNull(item);
            Assert.AreEqual(2, item.ChildElements.Count);

            //
            // The ordered list contains 3 items
            OrderedList orderedList = item.ChildElements[1] as OrderedList;

            Assert.IsNotNull(orderedList);
            Assert.AreEqual(3, orderedList.ChildElements.Count);

            //
            // Second list item, which contains two child items as well
            item = rootOrderedList.ChildElements[1] as ListItem;

            Assert.IsNotNull(item);
            Assert.AreEqual(2, item.ChildElements.Count);

            //
            // The ordered list contains 2 items
            orderedList = item.ChildElements[1] as OrderedList;

            Assert.IsNotNull(orderedList);
            Assert.AreEqual(2, orderedList.ChildElements.Count);
        }

        [Test()]
        public void ParseMixedList()
        {
            const string MixedListMarkup = "* Point one\r\n* Point two\r\n## Step 1\r\n## Step 2\r\n## Step 3\r\n* Point three\r\n** Sub point 1\r\n** Sub point 2\r\n";

            DomDocument document = new DomDocument();
            IBlockElementParser blockElementParser = new ListParser();

            blockElementParser.Parse(new TextileAuthoringEngine(), document, MixedListMarkup);

            //
            // Root unordered list
            UnorderedList rootUnorderedList = document.ChildElements[0] as UnorderedList;

            Assert.IsNotNull(rootUnorderedList);
            Assert.AreEqual(3, rootUnorderedList.ChildElements.Count);

            //
            // List item
            ListItem listItem = rootUnorderedList.ChildElements[1] as ListItem;

            Assert.IsNotNull(listItem);
            Assert.AreEqual(2, listItem.ChildElements.Count);

            //
            // Ordered list
            OrderedList orderedList = listItem.ChildElements[1] as OrderedList;

            Assert.IsNotNull(orderedList);
            Assert.AreEqual(3, orderedList.ChildElements.Count);

            //
            // One more list item
            listItem = rootUnorderedList.ChildElements[2] as ListItem;

            Assert.IsNotNull(listItem);
            Assert.AreEqual(2, listItem.ChildElements.Count);

            //
            // Unordered list
            UnorderedList unorderedList = listItem.ChildElements[1] as UnorderedList;

            Assert.IsNotNull(unorderedList);
            Assert.AreEqual(2, unorderedList.ChildElements.Count);
        }
    }
}
